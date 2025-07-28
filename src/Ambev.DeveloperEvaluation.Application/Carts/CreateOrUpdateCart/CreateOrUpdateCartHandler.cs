using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartHandler : IRequestHandler<CreateOrUpdateCartCommand>
{
    private readonly ICartService _cartService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateCartHandler> _logger;

    public CreateOrUpdateCartHandler(
        ICartService cartService,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<CreateOrUpdateCartHandler> logger)
    {
        _cartService = cartService;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateOrUpdateCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateOrUpdateCart] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        command.NormalizeItems();

        await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, cancellationToken);

        var cart = _mapper.Map<Cart>(command);

        await _cartService.CreateOrUpdateAsync(cart, cancellationToken);

        _logger.LogInformation("[CreateOrUpdateCart] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);
    }
}
