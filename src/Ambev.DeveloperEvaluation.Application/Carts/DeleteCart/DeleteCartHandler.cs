using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
{
    private readonly ICartService _cartService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly ILogger<DeleteCartHandler> _logger;

    public DeleteCartHandler(
        ICartService cartService,
        ICommandValidatorExecutor validatorExecutor,
        ILogger<DeleteCartHandler> logger)
    {
        _cartService = cartService;
        _validatorExecutor = validatorExecutor;
        _logger = logger;
    }

    public async Task Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[DeleteCart] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        await _validatorExecutor.ValidateAsync<DeleteCartCommandValidator, DeleteCartCommand>(command, cancellationToken);

        await _cartService.DeleteAsync(command, cancellationToken);

        _logger.LogInformation("[DeleteCart] Finsh - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);
    }
}
