using server.tests.Http;
using server.tests.Options;
using System.Text.Json;

namespace server.tests.v1.integrationTests
{
    public class QuotesControllerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly string _endpoint;

        public QuotesControllerIntegrationTests()
        {
            _client = HttpClientFactory.GetHttpClient(1);

            _serializerOptions = OptionsFactory.GetJsonSerializerOptions();

            _endpoint = "/api/seasons";
        }
    }
}
