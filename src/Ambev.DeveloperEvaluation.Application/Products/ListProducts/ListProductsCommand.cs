using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsCommand : IRequest<PaginatedResult<ProductResult>>, IProductQueryOptions
{
    public List<(string, bool)> OrderCriteria { get; set; } = new();
    public string? Category { get; set; }
    public string? Title { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
