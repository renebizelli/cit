using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

internal class CreateOrUpdateProductProfile : Profile
{
    public CreateOrUpdateProductProfile()
    {
        CreateMap<CreateOrUpdateProductCommand, Product>()
           .ForMember(f => f.Active, o => o.MapFrom(m => true));

        CreateMap<Product, CreateOrUpdateProductResult>()
            .ForMember(f => f.Rating, o => o.Ignore());

        CreateMap<Category, CreateOrUpdateProductCategoryResult>();

    }
}
