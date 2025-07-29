using Ambev.DeveloperEvaluation.Application.Products._Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductCommand : IRequest<ProductResult>
{
    public string Id { get; set; } = string.Empty;
}
