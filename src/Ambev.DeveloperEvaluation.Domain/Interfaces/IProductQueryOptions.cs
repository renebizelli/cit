namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IProductQueryOptions : IQueryOptions
{
    string? Category { get; set; }
    string? Title { get; set; }
    decimal MinPrice { get; set; }
    decimal MaxPrice { get; set; }
}
