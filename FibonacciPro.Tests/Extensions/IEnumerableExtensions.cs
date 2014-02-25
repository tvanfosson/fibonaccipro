using System;
using System.Collections.Generic;
using System.Linq;

namespace FibonacciPro.Tests.Extensions
{
    public static class IEnumerableExtensions
    {
        public static TSource SecondLast<TSource>(this IEnumerable<TSource> source)
        {
            return source.ToList()[source.Count() - 2];
        }
    }
}