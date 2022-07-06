using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using server.Models;
using System.Net;
using System.Text.Json;

namespace server.tests.integrationTests
{
    public class CharactersControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public CharactersControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();

            _client = webAppFactory.CreateDefaultClient();

            _client.DefaultRequestHeaders.Add("x-api-version", "1");

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _endpoint = "/api/characters";
        }

        [Fact]
        public async Task GetCharactersAsync_AllCharacters_Returns200StatusCodeWithCharacters()
        {
            var response = await _client.GetAsync(_endpoint);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetCharactersAsync_SeasonOneCharacters_Returns200StatusCodeWithCharacters()
        {
            var seasonValue = 1;

            var url = $"{_endpoint}?season={seasonValue}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);

            foreach (var character in characters)
            {
                character.Seasons.Should().Contain(seasonValue);
            }
        }

        [Fact]
        public async Task GetCharactersAsync_InvalidSeasonQueryParameter_Returns400StatusCodeWithValidationProblemDetails()
        {
            var seasonValue = "test";

            var url = $"{_endpoint}?season={seasonValue}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var details = JsonSerializer.Deserialize<ValidationProblemDetails>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            details.Should().NotBeNull();
            details.Should().BeOfType<ValidationProblemDetails>();
        }

        [Fact]
        public async Task GetCharactersAsync_NameContainsJason_Returns200StatusCodeWithCharacters()
        {
            var nameValue = "jason";

            var url = $"{_endpoint}?name={nameValue}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);

            foreach (var character in characters)
            {
                character.FullName.ToLower().Should().Contain(nameValue);
            }
        }

        [Fact]
        public async Task GetCharactersAsync_ActorNameContainsMandy_Returns200StatusCodeWithCharacters()
        {
            var actorNameValue = "mandy";

            var url = $"{_endpoint}?actorname={actorNameValue}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);

            foreach (var character in characters)
            {
                character.ActorFullName.ToLower().Should().Contain(actorNameValue);
            }
        }

        [Fact]
        public async Task GetCharacterByIdAsync_ValidCharacterId_Returns200StatusCodeWithCharacter()
        {
            var response = await _client.GetAsync(_endpoint);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            var characterId = characters[0].Id;

            var url = $"{_endpoint}/{characterId}";

            response = await _client.GetAsync(url);

            data = await response.Content.ReadAsStreamAsync();

            var character = JsonSerializer.Deserialize<Character>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            character.Should().NotBeNull();
            character.Should().BeOfType<Character>();
            character.Id.Should().Be(characterId);
        }

        [Fact]
        public async Task GetCharacterByIdAsync_InvalidCharacterId_Returns400StatusCodeWithValidationProblemDetails()
        {
            var characterId = "1";

            var url = $"{_endpoint}/{characterId}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var details = JsonSerializer.Deserialize<ValidationProblemDetails>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            details.Should().NotBeNull();
            details.Should().BeOfType<ValidationProblemDetails>();
            details.Detail.Should().NotBeNull();
            details.Errors.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCharacterByIdAsync_ValidCharacterIdForNonExistentCharacter_Returns404StatusCodeWithProblemDetails()
        {
            var characterId = ObjectId.GenerateNewId();

            var url = $"{_endpoint}/{characterId}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var details = JsonSerializer.Deserialize<ProblemDetails>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
            details.Detail.Should().NotBeNull();
        }
    }
}
