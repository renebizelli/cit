using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features._Shared;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(r => r.State)
            .MinimumLength(2)
            .MaximumLength(3)
            .WithMessage("State must be between 2 and 3 characters.");

        RuleFor(r => r.City)
            .MaximumLength(64)
            .WithMessage("City must be at most 64 characters.");

        RuleFor(r => r.Street)
            .MaximumLength(128)
            .When(r => !string.IsNullOrEmpty(r.Street))
            .WithMessage("Street must be at most 128 characters.");

        RuleFor(r => r.ZipCode)
            .MaximumLength(8)
            .When(r => !string.IsNullOrEmpty(r.ZipCode))
            .WithMessage("Zip code must be at most 8 characters.");

        RuleFor(r => r.Number)
            .MaximumLength(8)
            .When(r => !string.IsNullOrEmpty(r.Number))
            .WithMessage("Number must be at most 8 characters.");

        When(x => x.Geo != null, () =>
        {
            RuleFor(x => x.Geo)
                .SetValidator(new GeoLocationValidator())
                .WithMessage("Geolocation is invalid.");
        });
    }
}

public class GeoLocationValidator : AbstractValidator<AddressRequest.GeoLocation?>
{
    public GeoLocationValidator()
    {
        RuleFor(x => x!.Lat)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x!.Long)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");
    }
}