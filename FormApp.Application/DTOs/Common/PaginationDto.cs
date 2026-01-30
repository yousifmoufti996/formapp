namespace FormApp.Application.DTOs.Common;

public class PaginationRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public PaginationRequestDto()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationRequestDto(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
    }
}

public class PagedResultDto<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedResultDto(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
