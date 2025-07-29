using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Bogus;
using FluentAssertions;
using Xunit;

public class MinimumActiveSaleItemSpecificationTests
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
    public void IsSatisfiedBy_ReturnsTrue_WhenAtLeastOneActiveItem()
    {
        var sale = CreateSaleWithActiveItems(1);
        var spec = new MinimumActiveSaleItemSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsFalse_WhenNoItems()
    {
        var sale = CreateSaleWithActiveItems(0, 0);
        var spec = new MinimumActiveSaleItemSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsFalse_WhenOnlyCanceledItems()
    {
        var sale = CreateSaleWithActiveItems(0, 3);
        var spec = new MinimumActiveSaleItemSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsSatisfiedBy_ReturnsTrue_WhenActiveAndCanceledItems()
    {
        var sale = CreateSaleWithActiveItems(2, 2);
        var spec = new MinimumActiveSaleItemSpecification();

        var result = spec.IsSatisfiedBy(sale);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsSatisfiedBy_ThrowsArgumentNullException_WhenSaleIsNull()
    {
        var spec = new MinimumActiveSaleItemSpecification();

        Assert.Throws<ArgumentNullException>(() => spec.IsSatisfiedBy(null));
    }
}