using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Bogus;
using FluentAssertions;
using Xunit;

public class MinimumCartItemSpecificationTests
{
    private static Cart CreateCartWithItems(int itemCount)
    {
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<CartItem>()
        };

        for (int i = 0; i < itemCount; i++)
        {
            cart.Items.Add(new CartItem
            {
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 1
            });
        }

        return cart;
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsTrue_WhenAtLeastOneItem()
    {
        var cart = CreateCartWithItems(1);
        var spec = new MinimumCartItemSpecification();

        var result = spec.IsSatisfiedBy(cart);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsFalse_WhenNoItems()
    {
        var cart = CreateCartWithItems(0);
        var spec = new MinimumCartItemSpecification();

        var result = spec.IsSatisfiedBy(cart);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsTrue_WhenMultipleItems()
    {
        var cart = CreateCartWithItems(5);
        var spec = new MinimumCartItemSpecification();

        var result = spec.IsSatisfiedBy(cart);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsSatisfiedBy_ThrowsArgumentNullException_WhenCartIsNull()
    {
        var spec = new MinimumCartItemSpecification();

        Assert.Throws<ArgumentNullException>(() => spec.IsSatisfiedBy(null));
    }
}