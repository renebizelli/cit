using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

internal class CreateOrUpdateProfile : Profile
{
    public CreateOrUpdateProfile()
    {
        CreateMap<CreateOrUpdateCartCommand, Cart>();
        CreateMap<CreateOrUpdateCartCommand.CartItem, CartItem>();
        CreateMap<CreateOrUpdateCartCommand, CreateOrUpdateCartCommand>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src =>
                src.Items.GroupBy(p => p.ProductId)
                       .Select(g => new CreateOrUpdateCartCommand.CartItem
                       {
                           ProductId = g.Key,
                           Quantity = g.Sum(p => p.Quantity)
                       })
                       .ToList()));
    }
}
