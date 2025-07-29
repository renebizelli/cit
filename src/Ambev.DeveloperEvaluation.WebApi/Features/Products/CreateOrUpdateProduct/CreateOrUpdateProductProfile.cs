using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductProfile : Profile
{
    public CreateOrUpdateProductProfile()
    {
        CreateMap<CreateOrUpdateProductRequest, CreateOrUpdateProductCommand>();
        CreateMap<ProductResult, ProductResponse>();
        CreateMap<ProductCategoryResult, ProductCategoryResponse>();
        CreateMap<ProductRatingResult, ProductRatingResponse>();
    }
}
