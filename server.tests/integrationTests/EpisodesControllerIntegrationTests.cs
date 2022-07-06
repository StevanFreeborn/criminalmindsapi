using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using server.Models;
using System.Net;
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

        [Fact]
        public async Task GetEpisodesAsync_AllEpisodes_Returns200StatusCodeWithEpisodes()
        {
            var response = await _client.GetAsync(_endpoint);

            var data = await response.Content.ReadAsStreamAsync();

            var episodes = JsonSerializer.Deserialize<List<Episode>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("X-Rate-Limit-Limit");
            response.Headers.Should().ContainKey("X-Rate-Limit-Remaining");
            response.Headers.Should().ContainKey("X-Rate-Limit-Reset");

            episodes.Should().NotBeNull();
            episodes.Should().BeOfType<List<Episode>>();
            episodes.Should().HaveCountGreaterThan(0);
        }
    }
}
