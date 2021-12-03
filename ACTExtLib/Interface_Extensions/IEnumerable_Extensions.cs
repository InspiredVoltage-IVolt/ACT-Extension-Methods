namespace ACT.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>Iterates through a generic list type</summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="action"> </param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T obj in list.ToList<T>())
            {
                action(obj);
            }
        }

        /// <summary>Iterates through a list with a isFirst flag.</summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="action"> </param>
        public static void ForEachFirst<T>(this IEnumerable<T> list, Action<T, bool> action)
        {
            bool flag = true;
            foreach (T obj in list.ToList<T>())
            {
                action(obj, flag);
                flag = false;
            }
        }

        /// <summary>Iterates through a list with a index.</summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="action"> </param>
        public static void ForEachIndex<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            int num = 0;
            foreach (T obj in list.ToList<T>())
            {
                action(obj, num++);
            }
        }

        /// <summary>
        ///     If the <paramref name="currentEnumerable" /> is <see langword="null" /> , an Empty IEnumerable of <typeparamref name="T" /> is returned, else <paramref name="currentEnumerable" /> is returned.
        /// </summary>
        /// <param name="currentEnumerable"> The current enumerable. </param>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static IEnumerable<T> IfNullEmpty<T>(this IEnumerable<T> currentEnumerable) => currentEnumerable == null ? Enumerable.Empty<T>() : currentEnumerable;

        /// <summary>
        ///     Creates an infinite IEnumerable from the <paramref name="currentEnumerable" /> padding it with default( <typeparamref name="T" /> ).
        /// </summary>
        /// <param name="currentEnumerable"> The current enumerable. </param>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static IEnumerable<T> Infinite<T>(this IEnumerable<T> currentEnumerable)
        {
            foreach (T current in currentEnumerable)
            {
                T item = current;
                yield return item;
                item = default(T);
            }
            while (true)
            {
                yield return default(T);
            }
        }

        /// <summary>
        ///     Converts an <see cref="!:IEnumerable" /> to a HashSet -- similar to ToList()
        /// </summary>
        /// <param name="list"> The list. </param>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        /// <exception cref="T:System.ArgumentNullException"></exception>

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> list) => new HashSet<T>(list);
    }
}
