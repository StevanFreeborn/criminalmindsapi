using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace server.tests.integrationTests
{
    public class EpisodesControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public EpisodesControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();

            _client = webAppFactory.CreateDefaultClient();

            _client.DefaultRequestHeaders.Add("x-api-version", "1");

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _endpoint = "/api/episodes";
        }
    }
}
