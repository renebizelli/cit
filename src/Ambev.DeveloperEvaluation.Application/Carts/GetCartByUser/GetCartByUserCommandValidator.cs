using Ambev.DeveloperEvaluation.Application._Shared;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserCommandValidator : AbstractValidator<GetCartByUserCommand>
{
    public GetCartByUserCommandValidator()
    {
        Include(new UserBranchKeyValidator());
    }
}
