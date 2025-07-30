using Ambev.DeveloperEvaluation.Application.ProductRatings;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Rating;

public class ProductRatingProfile : Profile
{
    public ProductRatingProfile()
    {
        CreateMap<ProductRatingRequest, ProductRatingCommand>();
    }
}
