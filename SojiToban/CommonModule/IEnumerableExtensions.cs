using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.CommonModule
{
    static class IEnumerableExtensions
    {
        public static IOrderedEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            return Shuffle(source, RandomNumberGenerator.Create());
        }

        public static IOrderedEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source, RandomNumberGenerator rng)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var bytes = new byte[4];

            return source.OrderBy(delegate(TSource e)
            {
                rng.GetBytes(bytes);

                return BitConverter.ToInt32(bytes, 0);
            });
        }
    }
}
