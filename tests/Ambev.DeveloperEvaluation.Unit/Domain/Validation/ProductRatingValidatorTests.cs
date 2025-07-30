using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class ProductRatingValidatorTests
{
    private readonly ProductRatingValidator _validator = new ProductRatingValidator();

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void ValidRatings_ShouldNotHaveValidationError(int rating)
    {
        var result = _validator.TestValidate(rating);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(10)]
    public void InvalidRatings_ShouldHaveValidationError(int rating)
    {
        var result = _validator.TestValidate(rating);
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("The value must be between 1 and 5.");
    }
}