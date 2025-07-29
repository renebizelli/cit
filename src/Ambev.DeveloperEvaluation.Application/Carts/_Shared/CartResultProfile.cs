using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts._Shared;

public  class CartResultProfile : Profile
{
    public CartResultProfile()
    {
        CreateMap<Cart, CartResult>();
        CreateMap<CartItem, CartResult.CartItem>();
    }
}
