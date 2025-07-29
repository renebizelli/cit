using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserProfile : Profile
{
    public GetCartByUserProfile()
    {
        CreateMap<GetCartByUserRequest, GetCartByUserCommand>();
        CreateMap<CartResult, CartResponse>();
        CreateMap<CartResult.CartItem, CartResponse.CartItem>();
    }
}
