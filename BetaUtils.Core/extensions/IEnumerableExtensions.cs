using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BetaUtils.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Checks if IEnumerable is null or empty
        /// </summary>
        /// <typeparam name="items">IEnumerable</typeparam>
        /// <returns>Bool</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Iterates through IEnumerable
        /// </summary>
        /// <typeparam name="items">IEnumerable</typeparam>
        /// <typeparam name="action">Action</typeparam>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items is null)
            {
                return Enumerable.Empty<T>();
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in items)
            {
                action(item);
            }

            return items;
        }

        /// <summary>
        /// Converts an IEnumerable to a HashSet
        /// </summary>
        /// <typeparam name="items">IEnumerable</typeparam>
        /// <returns>HashSet</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            HashSet<T> hashSet = new HashSet<T>();

            foreach (var item in items)
            {
                hashSet.Add(item);
            }

            return hashSet;
        }

        /// <summary>
        /// Converts an IEnumerable to an Observable Collection
        /// </summary>
        /// <param name="items">IEnumerable</param>
        /// <returns>ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            var observableCollection = new ObservableCollection<T>();

            foreach (var item in items)
            {
                observableCollection.Add(item);
            }

            return observableCollection;
        }

        /// <summary>
        /// Takes a collection and splits it into a batch of collections with provided number of values
        /// </summary>
        /// <param name="items">IEnumerable</param>
        /// <param name="numberOfItemsPerBatch">Integer</param>
        /// <returns>New IEnumerable collections split into collections with provided number of items</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int numberOfItemsPerBatch)
        {
            return items.Select((item, inx) => new { item, inx })
                .GroupBy(x => x.inx / numberOfItemsPerBatch)
                .Select(x => x.Select(y => y.item));
        }

        /// <summary>
        /// Takes IEnumerable as a source and returns it with all the null values removed
        /// </summary>
        /// <param items="source">IEnumerable</param>
        /// <returns>IEnumerable without null values</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (T item in items)
            {
                if (item != null)
                {
                    yield return item;
                }
            }
        }

    }
}
