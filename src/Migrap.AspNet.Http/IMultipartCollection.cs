using System.Collections.Generic;

namespace Migrap.AspNet.Http {
    public interface IMultipartCollection : IReadOnlyList<IMultipart> {
        IMultipart this[string name] { get; }
        IMultipart GetPart(string name);
        IReadOnlyList<IMultipart> GetParts(string name);
    }
}