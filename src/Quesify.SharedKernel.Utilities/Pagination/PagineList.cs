namespace Quesify.SharedKernel.Utilities.Pagination;

public class PaginateList : IPaginateList
{
    public int Page { get; }

    public int Size { get; }
    
    public int TotalPages { get; }
    
    public int TotalCount { get; }
    
    public bool HasPreviousPage { get; }
    
    public bool HasNextPage { get; }
    
    public bool IsFirstPage { get; }
    
    public bool IsLastPage { get; }

    public PaginateList(
        int page,
        int size,
        int totalCount)
    {
        Page = page;
        Size = size;
        TotalPages = (int)Math.Ceiling(totalCount / (double)Size);
        TotalCount = totalCount;
        HasPreviousPage = page > 1;
        HasNextPage = page < TotalPages;
        IsFirstPage = page == 1;
        IsLastPage = page == TotalPages;
    }
}

public class PaginateList<T> : PaginateList, IPaginateList<T>
{
    public IReadOnlyCollection<T> Items { get; }

    public PaginateList(
        IReadOnlyCollection<T> items,
        int page,
        int size,
        int totalCount)
        : base(page, size, totalCount)
    {
        Items = items;
    }
}
