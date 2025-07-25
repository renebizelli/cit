using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Services;
public interface ICommandValidatorExecutor
{
    Task ValidateAsync<TValidator, TCommand>(TCommand command, CancellationToken cancellationToken)
        where TValidator : AbstractValidator<TCommand>, new()
        where TCommand : class;
}





