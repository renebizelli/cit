using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserCommandValidator : AbstractValidator<GetCartByUserCommand>
{
    public GetCartByUserCommandValidator()
    {
        Include(new CartKeyValidator());
    }
}
