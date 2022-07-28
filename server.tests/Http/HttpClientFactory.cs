using Microsoft.AspNetCore.Mvc.Testing;

namespace server.tests.Http
{
    internal static class HttpClientFactory
    {
        public static HttpClient GetClient(int version)
        {
            var webAppFactory = new WebApplicationFactory<Program>();

            var client = webAppFactory.CreateDefaultClient();

            client.DefaultRequestHeaders.Add("x-api-version", version.ToString());

            return client;
        }
    }
}
