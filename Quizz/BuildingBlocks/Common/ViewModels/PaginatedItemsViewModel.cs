using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels
{
    public class PaginatedItemsViewModel<T>
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        [Required]
        public IEnumerable<T> Data { get; private set; }

        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}