using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
    {
        return source.Aggregate((x, y) => selector(x).CompareTo(selector(y)) < 0 ? x : y);
    }
}
