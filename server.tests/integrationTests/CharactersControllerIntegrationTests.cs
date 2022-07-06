using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using server.Models;
using System.Collections;
using System.Collections.Generic;
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

            var characters = JsonSerializer.Deserialize<List<Character>>(data);

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
    }
}
