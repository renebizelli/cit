using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;
using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;
using Ambev.DeveloperEvaluation.WebApi.Features.Products._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        CreateMap<ListProductsRequest, ListProductsCommand>()
            .IncludeBase<ListSettingsRequest, ListSettings>();

        CreateMap<PaginatedResult<ProductResult>, Paginated<ProductResponse>>();
        CreateMap<ProductResult, ProductResponse>();
        CreateMap<ProductCategoryResult, ProductCategoryResponse>();
        CreateMap<ProductRatingResult, ProductRatingResponse>();
    }
}
