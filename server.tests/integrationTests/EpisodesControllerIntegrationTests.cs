using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using server.Models;
using System.Net;
using System.Text.Json;
using server.tests.Helpers;

namespace server.tests.IntegrationTests
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
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            episodes.Should().NotBeNull();
            episodes.Should().BeOfType<List<Episode>>();
            episodes.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetEpisodesAsync_SeasonOneEpisodes_Returns200StatusCodeWithEpisodes()
        {
            var seasonValue = 1;
            
            var url = $"{_endpoint}?season={seasonValue}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var episodes = JsonSerializer.Deserialize<List<Episode>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            episodes.Should().NotBeNull();
            episodes.Should().BeOfType<List<Episode>>();
            episodes.Should().HaveCountGreaterThan(0);

            foreach (var episode in episodes)
            {
                episode.Season.Should().Be(seasonValue);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_InvalidSeasonQueryParameter_Returns400StatusCodeWithValidationProblemDetails()
        {
            var seasonValue = "test";

            var url = $"{_endpoint}?season={seasonValue}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var details = JsonSerializer.Deserialize<ValidationProblemDetails>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            details.Should().NotBeNull();
            details.Should().BeOfType<ValidationProblemDetails>();
            details.Errors.Should().NotBeNull();
        }
    }
}
