using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

public class SaleItemQuantityWithinRangeSpecificationTests
{
    private readonly SaleItemQuantityWithinRangeSpecification _spec = new();

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(20)]
    public void IsSatisfiedBy_ReturnsTrue_ForValidQuantities(int quantity)
    {
        var result = _spec.IsSatisfiedBy(quantity);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void IsSatisfiedBy_ReturnsFalse_ForZeroOrNegativeQuantities(int quantity)
    {
        var result = _spec.IsSatisfiedBy(quantity);
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(21)]
    [InlineData(100)]
    [InlineData(int.MaxValue)]
    public void IsSatisfiedBy_ReturnsFalse_ForQuantitiesGreaterThanTwenty(int quantity)
    {
        var result = _spec.IsSatisfiedBy(quantity);
        result.Should().BeFalse();
    }
}