using AutoMapper;

namespace BoatApplication.Domain.Common.Models;

public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public PaginatedList(IReadOnlyCollection<T> items, int pageNumber, int pageSize, int totalPages, int totalCount)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = totalCount;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList<TDto> MapTo<TDto>(IMapper mapper)
    {
        var mappedItems = mapper.Map<List<TDto>>(Items);
        return new PaginatedList<TDto>(mappedItems, PageNumber, PageSize, TotalPages, TotalCount);
    }
}
