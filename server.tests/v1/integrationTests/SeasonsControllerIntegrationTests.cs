using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.tests.Helpers;
using server.tests.Http;
using server.tests.Options;
using System.Net;
using System.Text.Json;

namespace server.tests.v1.IntegrationTests
{
    public class SeasonsControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public SeasonsControllerIntegrationTests()
        {
            _client = HttpClientFactory.GetClient(1);

            _serializerOptions = OptionsFactory.GetJsonSerializerOptions();

            _endpoint = "/api/seasons";
        }

        [Fact]
        public async Task GetSeasonsAsync_AllSeasons_Returns200StatusCodeWithSeasons()
        {
            var response = await _client.GetAsync(_endpoint);

            var data = await response.Content.ReadAsStreamAsync();

            var seasons = JsonSerializer.Deserialize<List<Season>>(data, _serializerOptions);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            seasons.Should().NotBeNull();
            seasons.Should().BeOfType<List<Season>>();
            seasons.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_SeasonOne_Returns200StatusCodeWithSeason()
        {
            var seasonNumber = 1;

            var url = $"{_endpoint}/{seasonNumber}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var season = JsonSerializer.Deserialize<Season>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            season.Should().NotBeNull();
            season.Should().BeOfType<Season>();
            season.SeasonNumber.Should().Be(seasonNumber);
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_InvalidSeason_Returns404StatusCode()
        {
            var seasonNumber = "test";

            var url = $"{_endpoint}/{seasonNumber}";

            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_ValidSeasonNumberForNonExistentSeason_Returns404StatusCode()
        {
            var seasonNumber = 20;

            var url = $"{_endpoint}/{seasonNumber}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var details = JsonSerializer.Deserialize<ProblemDetails>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            details.Should().NotBeNull();
            details.Should().BeOfType<ProblemDetails>();
        }

    }
}
