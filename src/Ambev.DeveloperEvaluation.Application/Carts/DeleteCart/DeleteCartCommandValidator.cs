using Ambev.DeveloperEvaluation.Application._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        Include(new UserBranchKeyValidator());
    }
}
