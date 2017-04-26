using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Migrap.AspNet.Http {
    public static partial class HeaderDictionaryExtensions {
        public static string Allow(this IHeaderDictionary headers) {
            return nameof(Allow);
        }

        public static void Add(this IHeaderDictionary headers, string key, string value) {
            headers.Add(key, new string[] { value });
        }

        public static ContentDispositionHeaderValue GetContentDisposition(this IHeaderDictionary headers) {
            var contentDisposition = default(ContentDispositionHeaderValue);
            ContentDispositionHeaderValue.TryParse(headers[HeaderNames.ContentDisposition], out contentDisposition);
            return contentDisposition;
        }
    }
}