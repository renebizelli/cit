namespace Ambev.DeveloperEvaluation.Application._Shared;

public class PaginatedResult<T>
{
    public long TotalCount { get; set; }
    public List<T> Items { get; set; } = [];
}

