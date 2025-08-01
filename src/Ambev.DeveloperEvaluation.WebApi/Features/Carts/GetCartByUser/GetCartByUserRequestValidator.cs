﻿using Ambev.DeveloperEvaluation.WebApi.Features._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserRequestValidator : AbstractValidator<GetCartByUserRequest>
{
    public GetCartByUserRequestValidator()
    {
        Include(new UserBranchKeyRequestValidator());
    }
}
