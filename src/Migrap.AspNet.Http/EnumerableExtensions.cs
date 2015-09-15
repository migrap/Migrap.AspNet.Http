using System.Collections.Generic;

namespace Migrap.AspNet.Http {
    internal static class EnumerableExtensions {
        public static string Join(this IEnumerable<object> source, string seperator = ",") {
            return string.Join(seperator, source);
        }
    }
}