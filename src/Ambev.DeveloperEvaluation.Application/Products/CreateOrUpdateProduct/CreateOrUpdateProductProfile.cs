using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

internal class CreateOrUpdateProductProfile : Profile
{
    public CreateOrUpdateProductProfile()
    {
        CreateMap<CreateOrUpdateProductCommand, Product>()
           .ForMember(f => f.Category, o => o.MapFrom(m => new Category { Id = m.CategoryId }))
           .ForMember(f => f.Active, o => o.MapFrom(m => true));

        CreateMap<Product, ProductResult>()
            .ForMember(f => f.Rating, o => o.Ignore());

        CreateMap<Category, ProductCategoryResult>();

    }
}
