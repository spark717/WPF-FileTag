using System;
using System.Collections.Generic;
using System.Linq;

namespace FileTag.MVVM
{
    public static class SmartCollectionExtensions
    {
        /// <summary>
        /// Fill collection with new items. 
        /// If item already exists (check throw IComparer), replase it with existing instance.
        /// Else use CreationStrategy for items creating.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="source"></param>
        /// <param name="newItems">  </param>
        /// <param name="creationStartegy"></param>
        /// <param name="fill"> if true, refill existing collection, else treturn new collection </param>
        /// <returns></returns>
        public static List<T> SmartFill<T, I>(this List<T> source, IEnumerable<I> newItems, Func<I, T> creationStrategy, bool fill = true) where T: IComparable
        {
            var tempList = new List<T>();

            foreach (var newItem in newItems)
            {
                var existItem = source.FirstOrDefault(i => i.CompareTo(newItem) == 0);

                if (existItem != null)
                    tempList.Add(existItem);
                else
                    tempList.Add(creationStrategy(newItem));
            }

            if (fill)
            {
                source.Clear();
                source.AddRange(tempList);
                return source;
            }
            else
            {
                return new List<T>(tempList);
            }
        }
    }
}
