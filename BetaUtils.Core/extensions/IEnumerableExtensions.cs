using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BetaUtils.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Provides an extension method that iterates over the elements
        /// of an <see cref="IEnumerable{T}"/> and performs 
        /// the given <paramref name="action"/> on each element.
        /// </summary>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
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
        /// <returns>HashSet</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }

        /// <summary>
        /// Converts an IEnumerable to an Observable Collection
        /// </summary>
        /// <param name="items">IEnumerable</param>
        /// <returns>ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            return new ObservableCollection<T>(items);
        }

        /// <summary>
        /// Takes a collection and splits it into a batch of collections with provided number of values
        /// </summary>
        /// <returns>New IEnumerable collections split into collections with provided number of items</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int numberOfItemsPerBatch)
        {
            return items.Select((item, index) => new { item, index })
                .GroupBy(x => x.index / numberOfItemsPerBatch)
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
            return items.Where(item => item != null);
        }

    }
}
