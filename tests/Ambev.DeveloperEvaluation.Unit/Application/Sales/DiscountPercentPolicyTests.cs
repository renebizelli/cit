using Ambev.DeveloperEvaluation.Domain.Services.Sales.Discounts;
using static Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using FluentAssertions;
using Xunit;

public class DiscountPercentPolicyTests
{
    private static SaleProduct CreateProduct(decimal price)
        => new SaleProduct("prod1", "Product", price);

    private static SaleItem CreateSaleItem(decimal price, int quantity)
        => new SaleItem("item1", CreateProduct(price), quantity);

    [Theory]
    [InlineData(0.10, 100, 2, 20)]   // 10% of 200 = 20
    [InlineData(0.25, 50, 4, 50)]    // 25% of 200 = 50
    [InlineData(1.0, 10, 3, 30)]     // 100% of 30 = 30
    [InlineData(0.0, 99, 99, 0)]     // 0% discount
    public void Apply_ReturnsExpectedDiscount(decimal percent, decimal price, int quantity, decimal expected)
    {
        var policy = new DiscountPercentPolicy(percent);
        var item = CreateSaleItem(price, quantity);

        var discount = policy.Apply(item);

        discount.Should().Be(expected);
    }

    [Fact]
    public void Apply_ReturnsZero_WhenQuantityIsZero()
    {
        var policy = new DiscountPercentPolicy(0.5m);
        var item = CreateSaleItem(100, 0);

        var discount = policy.Apply(item);

        discount.Should().Be(0);
    }

    [Fact]
    public void Apply_ReturnsZero_WhenPriceIsZero()
    {
        var policy = new DiscountPercentPolicy(0.5m);
        var item = CreateSaleItem(0, 10);

        var discount = policy.Apply(item);

        discount.Should().Be(0);
    }

    [Fact]
    public void Apply_ReturnsNegativeDiscount_WhenNegativePercentage()
    {
        var policy = new DiscountPercentPolicy(-0.2m);
        var item = CreateSaleItem(100, 2);

        var discount = policy.Apply(item);

        discount.Should().Be(-40);
    }

    [Fact]
    public void Apply_ThrowsArgumentNullException_WhenSaleItemIsNull()
    {
        var policy = new DiscountPercentPolicy(0.1m);
        Assert.Throws<ArgumentNullException>(() => policy.Apply(null));
    }
}