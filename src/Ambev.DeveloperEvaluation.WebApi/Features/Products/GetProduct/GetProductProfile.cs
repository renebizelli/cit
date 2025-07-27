using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<GetProductRequest, GetProductCommand>();
        CreateMap<ProductResult, ProductResponse>();
        CreateMap<ProductCategoryResult, ProductCategoryResponse>();
        CreateMap<ProductRatingResult, ProductRatingResponse>();
    }
}
