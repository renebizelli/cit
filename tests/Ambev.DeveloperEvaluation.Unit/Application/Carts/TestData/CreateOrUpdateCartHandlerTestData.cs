using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;

public static class CreateOrUpdateCartHandlerTestData
{
    private static readonly Faker<CreateOrUpdateCartCommand> createOrUpdateCartCommandValid = new Faker<CreateOrUpdateCartCommand>()
    .RuleFor(u => u.UserId, f => Guid.NewGuid())
    .RuleFor(u => u.BranchId, f => Guid.NewGuid())
    .RuleFor(u => u.Items, f => new List<CreateOrUpdateCartCommand.CartItem>
    {
        new CreateOrUpdateCartCommand.CartItem { ProductId = 1, Quantity = 1 },
        new CreateOrUpdateCartCommand.CartItem { ProductId = 2, Quantity = 1 }
    });

    public static CreateOrUpdateCartCommand GenerateValidCommand() => createOrUpdateCartCommandValid.Generate();


    private static readonly Faker<CreateOrUpdateCartCommand> createOrUpdateCartCommandDenormalized = new Faker<CreateOrUpdateCartCommand>()
      .RuleFor(u => u.UserId, f => Guid.NewGuid())
      .RuleFor(u => u.BranchId, f => Guid.NewGuid())
      .RuleFor(u => u.Items, f => new List<CreateOrUpdateCartCommand.CartItem>
      {
        new CreateOrUpdateCartCommand.CartItem { ProductId = 1, Quantity = 1 },
        new CreateOrUpdateCartCommand.CartItem { ProductId = 1, Quantity = 1 }
      });

    public static CreateOrUpdateCartCommand GenerateDenormalizedCommand() => createOrUpdateCartCommandDenormalized.Generate();

}
