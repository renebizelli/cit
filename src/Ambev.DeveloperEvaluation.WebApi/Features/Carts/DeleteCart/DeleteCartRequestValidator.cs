using Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    public DeleteCartRequestValidator()
    {
        Include(new CartBaseRequestValidator());
    }
}
