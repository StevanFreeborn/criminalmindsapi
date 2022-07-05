using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using server.Models;
using System.Net;
using System.Text.Json;

namespace server.tests.integrationTests
{
    public class CharactersControllerIntegrationTests 
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public CharactersControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();

            _client = webAppFactory.CreateDefaultClient();

            _client.DefaultRequestHeaders.Add("x-api-version", "1");

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [Fact]
        public async Task GetCharacters_RetrieveAllCharacters_Returns200StatusCodeWithCharacters()
        {
            var response = await _client.GetAsync("/api/characters");

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Character>>(data);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            characters.Should().NotBeNull();
            characters.Should().HaveCountGreaterThan(0);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");
        }
        
        [Fact]
        public async Task GetCharacters_RetrieveSeasonOneCharacters_Returns200StatusCodeWithCharacters()
        {
            return;
        }
    }
}
