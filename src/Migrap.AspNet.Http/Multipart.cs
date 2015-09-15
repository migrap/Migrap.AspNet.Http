using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace Migrap.AspNet.Http {
    public class Multipart : IMultipart {
        private readonly Stream _stream;
        private readonly long _offset;
        private readonly long _length;

        public Multipart(Stream stream, long offset, long length) {
            _stream = stream;
            _offset = offset;
            _length = length;
        }

        public string ContentDisposition {
            get { return Headers[HeaderNames.ContentDisposition]; }
            set { Headers[HeaderNames.ContentDisposition] = value; }
        }

        public string ContentType {
            get { return Headers[HeaderNames.ContentType]; }
            set { Headers[HeaderNames.ContentType] = value; }
        }

        public IHeaderDictionary Headers { get; set; }

        public long Length => _length;

        public Stream OpenReadStream() {
            return new ReferenceReadStream(_stream, _offset, _length);
        }
    }
}
