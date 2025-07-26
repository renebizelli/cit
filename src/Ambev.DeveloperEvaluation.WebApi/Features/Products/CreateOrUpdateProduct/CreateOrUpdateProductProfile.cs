using Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductProfile : Profile
{
    public CreateOrUpdateProductProfile()
    {
        CreateMap<CreateOrUpdateProductRequest, CreateOrUpdateProductCommand>();
        CreateMap<CreateOrUpdateProductResult, CreateOrUpdateProductResponse>();
        CreateMap<CreateOrUpdateProductCategoryResult, CreateOrUpdateProductCategoryResponse>();
        CreateMap<CreateOrUpdateProductRatingResult, CreateOrUpdateProductRatingResponse>();
    }
}
