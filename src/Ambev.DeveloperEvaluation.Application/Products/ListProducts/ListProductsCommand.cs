using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsCommand : ListSettings, IRequest<PaginatedResult<ProductResult>>, IProductQuerySettings
{
    public string? Category { get; set; }
    public string? Title { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }

}
