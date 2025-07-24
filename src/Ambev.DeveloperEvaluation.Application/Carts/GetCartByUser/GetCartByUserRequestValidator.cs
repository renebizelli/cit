using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;

public class GetCartByUserCommandValidator : AbstractValidator<GetCartByUserCommand>
{
    public GetCartByUserCommandValidator()
    {
        Include(new CartKeyValidator());
    }
}
