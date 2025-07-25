using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        Include(new CartKeyValidator());
    }
}
