using System.Text;

namespace ACT.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Pic a Random Item from the Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static T PickRandom<T>(this IEnumerable<T> source, int v = 1) => source.PickRandom(v);

        /// <summary>
        /// Shuffle The Contents of the Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) => source.OrderBy<T, Guid>(x => Guid.NewGuid());

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

        /// <summary>
        /// Save Contents of a List to a File - Utilizes the ToString Method
        /// </summary>
        /// <typeparam name="T">Any IEnumerable</typeparam>
        /// <param name="list">List Of Data</param>
        /// <param name="FilePath">Place To Save File</param>
        /// <param name="AddNewLine">Add New Line After Every Entry</param>
        /// <returns>True/False</returns>
        public static bool SaveToFile<T>(this IEnumerable<T> list, string FilePath, bool AddNewLine = true)
        {
            StringBuilder _B = new StringBuilder();

            foreach (T item in list)
            {
                if (item == null) { continue; }
                _B.Append(item.ToString());
                if (AddNewLine) { _B.Append(Environment.NewLine); }
            }

            return _B.ToString().SaveAllText(FilePath);
        }
    }
}
