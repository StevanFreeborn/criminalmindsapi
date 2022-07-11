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
    public class QuotesControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public QuotesControllerIntegrationTests()
        {
            _client = HttpClientFactory.GetClient(1);

            _serializerOptions = OptionsFactory.GetJsonSerializerOptions();

            _endpoint = "/api/quotes";
        }

        [Fact]
        public async Task GetQuotesAsync_AllQuotes_Returns200StatusCodeWithQuotes()
        {
            var response = await _client.GetAsync(_endpoint);

            var data = await response.Content.ReadAsStreamAsync();

            var characters = JsonSerializer.Deserialize<List<Quote>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            characters.Should().NotBeNull();
            characters.Should().BeOfType<List<Quote>>();
            characters.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetQuotesAsync_SeasonOneQuotes_Returns200StatusCodeWithQoutes()
        {
            var season = 1;

            var url = $"{_endpoint}?{nameof(season)}={season}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var quotes = JsonSerializer.Deserialize<List<Quote>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            quotes.Should().NotBeNull();
            quotes.Should().BeOfType<List<Quote>>();
            quotes.Should().HaveCountGreaterThan(0);

            foreach (var quote in quotes)
            {
                quote.Season.Should().Be(season);
            }
        }

        [Fact]
        public async Task GetQuotesAsync_InvalidSeasonQueryParameter_Returns400StatusCodeWithValidationProblemDetails()
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
        public async Task GetQuotesAsync_EpisodeOneQuotes_Returns200StatusCodeWithQuotes()
        {
            var episode = 1;

            var url = $"{_endpoint}?{nameof(episode)}={episode}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var quotes = JsonSerializer.Deserialize<List<Quote>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            quotes.Should().NotBeNull();
            quotes.Should().BeOfType<List<Quote>>();
            quotes.Should().HaveCountGreaterThan(0);

            foreach (var quote in quotes)
            {
                quote.Episode.Should().Be(episode);
            }
        }

        [Fact]
        public async Task GetQuotesAsync_InvalidEpisodeQueryParameter_Returns400StatusCodeWithValidationProblemDetails()
        {
            var episode = "test";

            var url = $"{_endpoint}?{nameof(episode)}={episode}";

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
        public async Task GetQuotesAsync_QuotesThatContainEvil_Returns200StatusCodeWithQuotes()
        {
            var textKeyword = "evil";

            var url = $"{_endpoint}?{nameof(textKeyword)}={textKeyword}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var quotes = JsonSerializer.Deserialize<List<Quote>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            quotes.Should().NotBeNull();
            quotes.Should().BeOfType<List<Quote>>();
            quotes.Should().HaveCountGreaterThan(0);

            foreach (var quote in quotes)
            {
                quote.Text.ToLower().Should().Contain(textKeyword);
            }
        }

        [Fact]
        public async Task GetQuotesAsync_QuotesByYoda_Returns200StatusCodeWithQuotes()
        {
            var source = "yoda";

            var url = $"{_endpoint}?{nameof(source)}={source}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var quotes = JsonSerializer.Deserialize<List<Quote>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            quotes.Should().NotBeNull();
            quotes.Should().BeOfType<List<Quote>>();
            quotes.Should().HaveCountGreaterThan(0);

            foreach (var quote in quotes)
            {
                quote.Source.ToLower().Should().Contain(source);
            }
        }

        [Fact]
        public async Task GetQuotesAsync_QuotesNarratedByJason_Returns200StatusCodeWithQuotes()
        {
            var narrator = "jason";

            var url = $"{_endpoint}?{nameof(narrator)}={narrator}";

            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStreamAsync();

            var quotes = JsonSerializer.Deserialize<List<Quote>>(data, _serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            AssertHelper.CheckForRateLimitingHeaders(response.Headers);

            quotes.Should().NotBeNull();
            quotes.Should().BeOfType<List<Quote>>();
            quotes.Should().HaveCountGreaterThan(0);

            foreach (var quote in quotes)
            {
                quote.Narrator.ToLower().Should().Contain(narrator);
            }
        }
    }
}
