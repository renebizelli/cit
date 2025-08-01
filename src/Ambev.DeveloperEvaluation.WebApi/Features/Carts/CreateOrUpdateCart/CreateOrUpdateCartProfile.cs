﻿using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartProfile : Profile
{
    public CreateOrUpdateCartProfile()
    {
        CreateMap<CreateOrUpdateCartRequest, CreateOrUpdateCartCommand>()
                .ForMember(f => f.UserId, v => v.MapFrom(f => f.UserId))
                .ForMember(f => f.BranchId, v => v.MapFrom(f => f.BranchId))
                .ForMember(f => f.UpdatedAt, v => v.MapFrom(f => DateTime.Now));

        CreateMap<CreateOrUpdateCartRequest.CartItem, CreateOrUpdateCartCommand.CartItem>();

        CreateMap<CartResult, CartResponse>();
        CreateMap<CartResult.CartItem, CartResponse.CartItem>();

    }
}
