﻿using Ambev.DeveloperEvaluation.Application.Products._Shared;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductCommand : IRequest<ProductResult>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}
