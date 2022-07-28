using FluentAssertions;
using System.Net.Http.Headers;

namespace server.tests.Helpers
{
    internal static class AssertHelper
    {
        public static void CheckForRateLimitingHeaders(HttpResponseHeaders headers)
        {
            headers.Should().ContainKey("X-Rate-Limit-Limit");
            headers.Should().ContainKey("X-Rate-Limit-Remaining");
            headers.Should().ContainKey("X-Rate-Limit-Reset");
        }
    }
}
