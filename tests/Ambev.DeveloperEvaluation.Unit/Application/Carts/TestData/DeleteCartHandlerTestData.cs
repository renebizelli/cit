using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;

public static class DeleteCartHandlerTestData
{
    private static readonly Faker<DeleteCartCommand> deleteCartCommandValid = new Faker<DeleteCartCommand>()
    .RuleFor(u => u.UserId, f => Guid.NewGuid())
    .RuleFor(u => u.BranchId, f => Guid.NewGuid());

    public static DeleteCartCommand GenerateValidCommand() => deleteCartCommandValid.Generate();
}
