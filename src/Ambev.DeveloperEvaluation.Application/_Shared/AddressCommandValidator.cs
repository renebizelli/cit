using FluentValidation;
using static Ambev.DeveloperEvaluation.Application._Shared.AddressCommand;

namespace Ambev.DeveloperEvaluation.Application._Shared;

public class AddressCommandValidator : AbstractValidator<AddressCommand>
{
    public AddressCommandValidator()
    {
         RuleFor(r => r.State)
            .MinimumLength(2)
            .MaximumLength(3)
            .WithMessage("##TODO: State must be between 2 and 3 characters long.");

        RuleFor(r => r.City)
           .MaximumLength(64)
           .WithMessage("##TODO: City must be max 64 characters long.");

        RuleFor(r => r.Street)
           .MaximumLength(128)
           .When(r => !string.IsNullOrEmpty(r.Street))
           .WithMessage("##TODO: Street must be max 64 characters long.");

        RuleFor(r => r.ZipCode)
           .MaximumLength(8)
           .When(r => !string.IsNullOrEmpty(r.ZipCode))
           .WithMessage("##TODO: ZipCode must be max 8 characters long.");

        RuleFor(r => r.Number)
           .MaximumLength(8)
           .When(r => !string.IsNullOrEmpty(r.Number))
           .WithMessage("##TODO: Number must be max 8 characters long.");

        When(x => x.Geo != null, () =>
        {
            RuleFor(x => x.Geo)
                .SetValidator(new GeoLocationValidator())
                .WithMessage("Longitude deve estar entre -180 e 180.");
        });

    }
}

public class GeoLocationValidator : AbstractValidator<AddressCommand.GeoLocation?>
{
    public GeoLocationValidator()
    {
        RuleFor(x => x!.Lat)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude deve estar entre -90 e 90.");

        RuleFor(x => x!.Long)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude deve estar entre -180 e 180.");
    }
}