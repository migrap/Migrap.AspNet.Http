using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace Migrap.AspNet.Http {
    public class MultipartCollection : List<IMultipart>, IMultipartCollection {
        public IMultipart this[string name] {
            get { return GetPart(name); }
        }

        public IMultipart GetPart(string name) {
            return Find(part => string.Equals(name, GetName(part.ContentDisposition)));
        }

        public IReadOnlyList<IMultipart> GetParts(string name) {
            return FindAll(part => string.Equals(name, GetName(part.ContentDisposition)));
        }

        private static string GetName(string contentDisposition) {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            ContentDispositionHeaderValue cd;
            ContentDispositionHeaderValue.TryParse(contentDisposition, out cd);
            return HeaderUtilities.RemoveQuotes(cd?.Name);
        }
    }
}