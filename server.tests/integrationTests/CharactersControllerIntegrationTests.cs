using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace server.tests.integrationTests
{
    public class CharactersControllerIntegrationTests 
    {
        private readonly HttpClient _client;

        public CharactersControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _client = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetCharacters_RetrievesAllCharacters_Returns200StatusCodeWithCharacters()
        {
            var response = await _client.GetAsync("/api/characters");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
