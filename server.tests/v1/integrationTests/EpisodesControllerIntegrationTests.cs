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
    public class EpisodesControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public EpisodesControllerIntegrationTests()
        {
            _client = HttpClientFactory.GetClient(1);

            _serializerOptions = OptionsFactory.GetJsonSerializerOptions();

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
            var season = 1;

            var url = $"{_endpoint}?{nameof(season)}={season}";

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
                episode.Season.Should().Be(season);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_InvalidSeasonQueryParameter_Returns400StatusCodeWithValidationProblemDetails()
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
        public async Task GetEpisodesAsync_EpisodesAfterJanuary2020_Returns200StatusCodeWithEpisodes()
        {
            var startDate = new DateTime(2020, 1, 1);

            var url = $"{_endpoint}?{nameof(startDate)}={startDate}";

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
                episode.AirDate.Should().BeOnOrAfter(startDate);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_InvalidStartDateQueryParam_Returns400StatusCodeWithValidationProblemDetails()
        {
            var startDate = "test";

            var url = $"{_endpoint}?{nameof(startDate)}={startDate}";

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
        public async Task GetEpisodesAsync_EpisodesBeforeJanuary2020_Returns200StatusCodeWithEpisodes()
        {
            var endDate = new DateTime(2020, 1, 1);

            var url = $"{_endpoint}?{nameof(endDate)}={endDate}";

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
                episode.AirDate.Should().BeOnOrBefore(endDate);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_InvalidEndDateQueryParam_Returns400StatusCodeWithValidationProblemDetails()
        {
            var endDate = "test";

            var url = $"{_endpoint}?{nameof(endDate)}={endDate}";

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
        public async Task GetEpisodesAsync_EpisodeTitlesThatContainThe_Returns200StatusCodeWithEpisodes()
        {
            var title = "the";

            var url = $"{_endpoint}?{nameof(title)}={title}";

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
                episode.Title.ToLower().Should().Contain(title);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_EpisodeSummariesThatContainFoyet_Returns200StatusCodeWithEpisodes()
        {
            var summaryKeyword = "foyet";

            var url = $"{_endpoint}?{nameof(summaryKeyword)}={summaryKeyword}";

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
                episode.Summary.ToLower().Should().Contain(summaryKeyword);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_EpisodesDirectedByCharles_Returns200StatusCodeWithEpisodes()
        {
            var directedBy = "charles";

            var url = $"{_endpoint}?{nameof(directedBy)}={directedBy}";

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
                episode.DirectedBy.ToLower().Should().Contain(directedBy);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_EpisodesWrittenByBreen_Returns200StatusCodeWithEpisodes()
        {
            var writtenBy = "breen";

            var url = $"{_endpoint}?{nameof(writtenBy)}={writtenBy}";

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
                episode.WrittenBy.Any(writer => writer.ToLower().Contains(writtenBy)).Should().BeTrue();
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_EpisodesWithGreaterThan12MillionUsViewers_Returns200StatusCodeWithEpisodes()
        {
            var viewersRangeStart = 12.0;

            var url = $"{_endpoint}?{nameof(viewersRangeStart)}={viewersRangeStart}";

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
                episode.UsViewersInMillions.Should().BeGreaterThanOrEqualTo(viewersRangeStart);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_InvalidViewersRangeStartQueryParam_Returns400StatusCodeWithValidationProblemDetails()
        {
            var viewersRangeStart = "test";

            var url = $"{_endpoint}?{nameof(viewersRangeStart)}={viewersRangeStart}";

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
        public async Task GetEpisodesAsync_EpisodesWithLessThan12MillionUsViewers_Returns200StatusCodeWithEpisodes()
        {
            var viewersRangeEnd = 12.0;

            var url = $"{_endpoint}?{nameof(viewersRangeEnd)}={viewersRangeEnd}";

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
                episode.UsViewersInMillions.Should().BeLessThanOrEqualTo(viewersRangeEnd);
            }
        }

        [Fact]
        public async Task GetEpisodesAsync_InvalidViewersRangeEndQueryParam_Returns400StatusCodeWithValidationProblemDetails()
        {
            var viewersRangeEnd = "test";

            var url = $"{_endpoint}?{nameof(viewersRangeEnd)}={viewersRangeEnd}";

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
        public async Task GetEpisodeByNumberAsync_ValidEpisodeNumber_Returns200StatusCodeWithEpisode()
        {
            var episodeNumber = 1;

            var url = $"{_endpoint}/{episodeNumber}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var episode = JsonSerializer.Deserialize<Episode>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            episode.Should().NotBeNull();
            episode.Should().BeOfType<Episode>();
            episode.NumberInSeries.Should().Be(episodeNumber);
        }

        [Fact]
        public async Task GetEpisodeByNumberAsync_InvalidEpisodeNumber_Returns404StatusCode()
        {
            var episodeNumber = "test";

            var url = $"{_endpoint}/{episodeNumber}";

            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);
        }

        [Fact]
        public async Task GetEpisodeByNumberAsync_ValidEpisodeNumberForNonExistentEpisode_Returns404StatusCodeWithProblemDetails()
        {
            var episodeNumber = 10000;

            var url = $"{_endpoint}/{episodeNumber}";

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
