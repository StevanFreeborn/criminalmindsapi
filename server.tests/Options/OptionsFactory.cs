using System.Text.Json;

namespace server.tests.Options
{
    internal static class OptionsFactory
    {
        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }
}
