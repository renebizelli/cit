using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public  class NormalizeItemsTest
{

    [Fact(DisplayName = "Given a command with duplicate items, when creating, it must be normalized.")]
    public void CommandWithDuplicateItemsNormalize()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateDenormalizedCommand();

        command.NormalizeItems();

        Assert.Equal(2, command.Items.First().Quantity);
    }

    [Fact(DisplayName = "Given a valid command, when creating, it must preserve the same items.")]
    public void ValidCommandPreserveItems()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();

        command.NormalizeItems();

        Assert.Equal(2, command.Items.Count);
        Assert.True(command.Items.Where(a => a.ProductId.Equals("1111") && a.Quantity.Equals(1)).Any());
        Assert.True(command.Items.Where(a => a.ProductId.Equals("2222") && a.Quantity.Equals(1)).Any());
    }


}
