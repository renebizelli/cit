using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Bogus;
using FluentAssertions;
using Xunit;

public class AllowedDiscountSpecificationTests
{
    private static Sale CreateSaleWithActiveItems(int activeCount, int canceledCount = 0)
    {
        var sale = new Sale(
            id: Guid.NewGuid().ToString(),
            saleNumber: new Faker().Random.Long(1, 1000),
            branch: new Sale.SaleBranch(Guid.NewGuid(), "Branch"),
            user: new Sale.SaleUser(Guid.NewGuid(), "User", "City", "State")
        );

        for (int i = 0; i < activeCount; i++)
        {
            var item = new Sale.SaleItem(
                id: Guid.NewGuid().ToString(),
                product: new Sale.SaleProduct(Guid.NewGuid().ToString(), "Product", 10m),
                quantity: 1
            );
            sale.AddItem(item);
        }

        for (int i = 0; i < canceledCount; i++)
        {
            var item = new Sale.SaleItem(
                id: Guid.NewGuid().ToString(),
                product: new Sale.SaleProduct(Guid.NewGuid().ToString(), "Product", 10m),
                quantity: 1
            );

            item.Cancel();

            sale.AddItem(item);
        }

        return sale;
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsTrue_WhenActiveItemsGreaterThanFour()
    {
        var sale = CreateSaleWithActiveItems(5);
        var spec = new AllowedDiscountSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(4)]
    [InlineData(3)]
    [InlineData(0)]
    public void IsSatisfiedBy_ReturnsFalse_WhenActiveItemsLessThanOrEqualToFour(int activeCount)
    {
        var sale = CreateSaleWithActiveItems(activeCount);
        var spec = new AllowedDiscountSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsSatisfiedBy_IgnoresCanceledItems()
    {
        var sale = CreateSaleWithActiveItems(5, canceledCount: 3);
        var spec = new AllowedDiscountSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsFalse_WhenNoActiveItemsButHasCanceledItems()
    {
        var sale = CreateSaleWithActiveItems(0, canceledCount: 5);
        var spec = new AllowedDiscountSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsSatisfiedBy_ThrowsArgumentNullException_WhenSaleIsNull()
    {
        var spec = new AllowedDiscountSpecification();

        Assert.Throws<ArgumentNullException>(() => spec.IsSatisfiedBy(null));
    }
}