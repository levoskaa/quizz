using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quizz.Common.ViewModels;

public class PaginatedItemsViewModel<T>
{
    [Required]
    public int PageIndex { get; private set; }

    [Required]
    public int PageSize { get; private set; }

    [Required]
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
