using System.Collections.Generic;
using System.Linq;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static void RemoveAll<T>(this IList<T> list, Func<T, bool> selector)
        {
            var itemsToRemove = list.Where(selector).ToList();
            itemsToRemove.ForEach(i => list.Remove(i));
        }
    }
}