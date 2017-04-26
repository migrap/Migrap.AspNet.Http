using Microsoft.AspNetCore.Http;
using System.IO;

namespace Migrap.AspNet.Http {
    public interface IMultipart {
        string ContentType { get; }
        string ContentDisposition { get; }
        IHeaderDictionary Headers { get; }
        long Length { get; }
        Stream OpenReadStream();
    }
}