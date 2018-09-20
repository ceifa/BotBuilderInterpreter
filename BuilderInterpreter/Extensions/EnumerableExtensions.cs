using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Extensions
{
    internal static class EnumerableExtensions
    {
        public static async Task<bool> AllAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
        {
            foreach (var item in source)
            {
                if (!await predicate(item))
                    return false;
            }

            return true;
        }
    }
}
