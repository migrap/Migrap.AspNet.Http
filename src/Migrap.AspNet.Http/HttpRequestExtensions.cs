using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Migrap.AspNet.Http {
    public static class HttpRequestExtensions
    {
        public static IMultipartCollection ReadMultipart(this HttpRequest request) {
            return ReadMultipartAsync(request, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static async Task<IMultipartCollection> ReadMultipartAsync(this HttpRequest request, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();

            request.EnableRewind();

            var parts = new MultipartCollection();

            using(cancellationToken.Register(request.HttpContext.Abort)) {
                var contentType = GetContentType(request);
                var boundary = GetBoundary(contentType);

                var multipartReader = new MultipartReader(boundary, request.Body);
                var section = await multipartReader.ReadNextSectionAsync(cancellationToken);
                while(section != null) {
                    var headers = new HeaderDictionary(section.Headers);
                    var contentDisposition = headers.GetContentDisposition();

                    await section.Body.DrainAsync(cancellationToken);

                    var part = new Multipart(request.Body, section.BaseStreamOffset.Value, section.Body.Length) {
                        Headers = headers
                    };
                    parts.Add(part);

                    section = await multipartReader.ReadNextSectionAsync(cancellationToken);
                }

            }

            request.Body.Seek(0, SeekOrigin.Begin);

            return parts;
        }

        public static Task<IMultipartCollection> ReadPartsAsync(this HttpRequest request, CancellationToken cancellationToken = default(CancellationToken)) {
            return ReadMultipartAsync(request, cancellationToken);
        }

        private static MediaTypeHeaderValue GetContentType(HttpRequest request) {
            return MediaTypeHeaderValue.Parse(request.ContentType);
        }

        private static string GetBoundary(MediaTypeHeaderValue contentType) {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
            if(string.IsNullOrWhiteSpace(boundary)) {
                throw new InvalidOperationException("Missing content-type boundary.");
            }
            return boundary;
        }
    }
}
