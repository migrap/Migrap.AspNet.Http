using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;

namespace Migrap.AspNet.Http {
    public static class HttpResponseExtensions {
        public static void Headers(this HttpResponse response, Func<IHeaderDictionary, Func<string>> header, params string[] value) {
            new { header, value }.CheckNotNull();
            response.Headers.Add(header(response.Headers)(), value.Join());
        }        
    }
}
