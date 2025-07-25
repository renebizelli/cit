using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;

public static class GetCartByUserHandlerTestData
{
    private static readonly Faker<GetCartByUserCommand> getCartByUserCommandValid = new Faker<GetCartByUserCommand>()
    .RuleFor(u => u.UserId, f => Guid.NewGuid())
    .RuleFor(u => u.BranchId, f => Guid.NewGuid());

    public static GetCartByUserCommand GenerateValidCommand() => getCartByUserCommandValid.Generate();
}
