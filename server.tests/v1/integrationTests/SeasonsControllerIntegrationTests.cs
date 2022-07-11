using server.tests.Http;
using server.tests.Options;
using System.Text.Json;

namespace server.tests.v1.integrationTests
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
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_SeasonOne_Returns200StatusCodeWithSeason()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_InvalidSeason_Returns404StatusCode()
        { 
            throw new NotImplementedException();
        }

        [Fact]
        public async Task GetSeasonByNumberAsync_ValidSeasonNumberForNonExistentSeason_Returns404StatusCode()
        {
            throw new NotImplementedException();
        }

    }
}
