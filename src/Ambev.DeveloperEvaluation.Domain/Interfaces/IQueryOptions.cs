namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IQueryOptions
{
    List<(string, bool)> OrderCriteria { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
}
