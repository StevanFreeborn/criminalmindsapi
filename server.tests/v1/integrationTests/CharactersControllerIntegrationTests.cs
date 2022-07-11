using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using server.Models;
using System.Net;
using System.Text.Json;
using server.tests.Helpers;
using server.tests.Http;
using server.tests.Options;

namespace server.tests.v1.IntegrationTests
{
    public class CharactersControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public CharactersControllerIntegrationTests()
        {
            _client = HttpClientFactory.GetClient(1);

            _serializerOptions = OptionsFactory.GetJsonSerializerOptions();

            _endpoint = "/api/characters";
        }

        [Fact]
        public async Task GetCharactersAsync_AllCharacters_Returns200StatusCodeWithCharacters()
        {
            var response = await _client.GetAsync(_endpoint);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetCharactersAsync_SeasonOneCharacters_Returns200StatusCodeWithCharacters()
        {
            var season = 1;

            var url = $"{_endpoint}?{nameof(season)}={season}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);

            foreach (var character in characters)
            {
                character.Seasons.Should().Contain(season);
            }
        }

        [Fact]
        public async Task GetCharactersAsync_InvalidSeasonQueryParameter_Returns400StatusCodeWithValidationProblemDetails()
        {
            var season = "test";

            var url = $"{_endpoint}?{nameof(season)}={season}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var details = JsonSerializer.Deserialize<ValidationProblemDetails>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            details.Should().NotBeNull();
            details.Should().BeOfType<ValidationProblemDetails>();
            details.Errors.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCharactersAsync_NameContainsJason_Returns200StatusCodeWithCharacters()
        {
            var name = "jason";

            var url = $"{_endpoint}?{nameof(name)}={name}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);

            foreach (var character in characters)
            {
                character.FullName.ToLower().Should().Contain(name);
            }
        }

        [Fact]
        public async Task GetCharactersAsync_ActorNameContainsMandy_Returns200StatusCodeWithCharacters()
        {
            var actorName = "mandy";

            var url = $"{_endpoint}?{nameof(actorName)}={actorName}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Character>>();
            characters.Should().HaveCountGreaterThan(0);

            foreach (var character in characters)
            {
                character.ActorFullName.ToLower().Should().Contain(actorName);
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
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

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
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

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
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
            details.Detail.Should().NotBeNull();
        }
    }
}
