using System;
using System.Collections;
using System.Collections.Generic;

namespace BetaUtils.Core.Collections
{
    public class PaginatedCollection<T> : IPaginatedCollection<T> where T : class
    {
        public PaginatedCollection()
        {
            TotalItemsCount = 0;
            CurrentPageNumber = 0;
            ItemsPerPage = 0;
            _items = null;
        }

        public PaginatedCollection(IEnumerable<T> items)
        {
            int result = 0;
            using (IEnumerator<T> enumerator = items.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    result++;
            }
            _items = new T[result];

            using (IEnumerator<T> enumerator = items.GetEnumerator())
            {
                enumerator.MoveNext();
                for (int i = 0; i < result; i++)
                {
                    _items[i] = enumerator.Current;
                    enumerator.MoveNext();
                }
            }
        }

        public PaginatedCollection(IEnumerable<T> items, int totalItemsCount, int currentPageNumber, int itemsPerPage)
            : this(items)
        {
            TotalItemsCount = totalItemsCount;
            CurrentPageNumber = currentPageNumber;
            ItemsPerPage = itemsPerPage;
        }

        public PaginatedCollection(PaginatedCollection<T> paginatedCollection)
        {
            _items = paginatedCollection.Items;
            TotalItemsCount = paginatedCollection.TotalItemsCount;
            CurrentPageNumber = paginatedCollection.CurrentPageNumber;
            ItemsPerPage = paginatedCollection.ItemsPerPage;
        }

        private readonly T[] _items;

        public T[] Items { get => _items;  }
        public int TotalItemsCount { get; set; }
        public int CurrentPageNumber { get; set; }
        public int ItemsPerPage { get; set; }

        public bool HasPrevious => CurrentPageNumber > 0;

        public bool HasNext => CurrentPageNumber < TotalPageCount;

        public int TotalPageCount => (int)Math.Ceiling((decimal)TotalItemsCount / ItemsPerPage);

        public TEnum<T> GetEnumerator()
        {
            return new TEnum<T>(_items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>) GetEnumerator();
        }
    }

    public class TEnum<T> : IEnumerator where T : class
    {

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public TEnum(T[] list)
        {
            Items = list;
        }

        public bool MoveNext()
        {
            position++;
            return position < Items.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return Items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public T[] Items { get; set; }
    }

}
