﻿using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserProfile : Profile
{
    public GetCartByUserProfile()
    {
        CreateMap<GetCartByUserCommand, Cart>();
    }
}
