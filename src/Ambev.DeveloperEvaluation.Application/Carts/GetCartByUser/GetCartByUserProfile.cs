using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserProfile : Profile
{
    public GetCartByUserProfile()
    {
        CreateMap<Cart, GetCartByUserResult>();
        CreateMap<CartItem, GetCartByUserResult.CartItem>();
    }
}
