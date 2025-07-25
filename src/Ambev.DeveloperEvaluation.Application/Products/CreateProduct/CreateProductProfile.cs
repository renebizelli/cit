using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

internal class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
           .ForMember(f => f.Active, o => o.MapFrom(m => true));

        CreateMap<Product, CreateProductResult>()
            .ForMember(f => f.Rating, o => o.Ignore());

        CreateMap<Category, CreateProductCategoryResult>();

    }
}
