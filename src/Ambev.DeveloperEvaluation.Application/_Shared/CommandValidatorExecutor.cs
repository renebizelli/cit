using Ambev.DeveloperEvaluation.Domain.Services;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application._Shared;

public class CommandValidatorExecutor : ICommandValidatorExecutor
{
    public async Task ValidateAsync<TValidator, TRequest>(TRequest command, CancellationToken cancellationToken)
    where TValidator : AbstractValidator<TRequest>, new()
    where TRequest : class
    {
        var validator = new TValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
