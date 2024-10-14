using System.Collections.Generic;

namespace Catalog.API.Application.Request;

public record QueryBase
{
    private const int MaxPageSize = 50;
    private int _defaultSize = 15;
    private string _searchText;
    public string SearchText { get => _searchText; set => _searchText = value.ToLower(); }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get => _defaultSize; set => _defaultSize = (value > MaxPageSize) ? MaxPageSize : value; }
}

public record ProductQueryFilter : QueryBase
{
    public IEnumerable<string> CategoryIds { get; set; } = [];
    public IEnumerable<string> AttributeIds { get; set; } = [];
    public string BrandId { get; set; } = default!;
}