using Microsoft.AspNetCore.Http;

namespace Aliases
{
    public static class HttpRequestExtensions
    {
        public static string GetApiVersion(this HttpRequest request)
        {
            return request.ContainsApiVersion() ? request.Headers["api-version"].ToString() : string.Empty;
        }

        public static bool ContainsApiVersion(this HttpRequest request)
        {
            return request.Headers.ContainsKey("api-version");
        }
    }
}
