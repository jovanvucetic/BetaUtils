using System.Collections.Generic;

namespace BetaUtils.Core.Collections
{
    public interface IPaginatedCollection<out T> : IEnumerable<T>
    {
        int TotalItemsCount { get; set; }
        int CurrentPageNumber { get; set; }
        int ItemsPerPage { get; set; }
        bool HasPrevious { get; }
        bool HasNext { get; }
        int TotalPageCount { get; }
    }
}
