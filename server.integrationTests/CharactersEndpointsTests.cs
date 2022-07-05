using Microsoft.AspNetCore.Mvc.Testing;
using server.Models;
using System.Net;
using System.Text.Json;

namespace server.integrationTests
{
    [TestClass]
    public class CharacterEndpointsTests
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        public TestContext? TestContext { get; set; }

        public CharacterEndpointsTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();

            _httpClient = webAppFactory.CreateDefaultClient();

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [TestMethod]
        public async Task GetCharacters_AllCharacters_Returns200StatusWithCharacters()
        {
            var response = await _httpClient.GetAsync("/api/characters");
            var data = await response.Content.ReadAsStringAsync();
            var characters = JsonSerializer.Deserialize<List<Character>>(data, _serializerOptions);
            
            Assert.IsNotNull(characters);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}