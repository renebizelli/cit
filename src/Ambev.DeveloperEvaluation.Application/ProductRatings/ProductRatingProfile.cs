using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.ProductRatings;

public class ProductRatingProfile : Profile
{
    public ProductRatingProfile()
    {
        CreateMap<ProductRatingCommand, Domain.Entities.ProductRating>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));   
    }
}
