using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartHandler : IRequestHandler<CreateOrUpdateCartCommand>
{
    private readonly ICacheRepository _cache;
    private readonly ICartRepository _repository;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateCartHandler> _logger;

    public CreateOrUpdateCartHandler(
        ICacheRepository cache,
        ICartRepository cart,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<CreateOrUpdateCartHandler> logger)
    {
        _repository = cart;
        _cache = cache;
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

        await CreateOrUpdateCartWithCacheAsync(command, cart, cancellationToken);

        _logger.LogInformation("[CreateOrUpdateCart] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);
    }

    private async Task CreateOrUpdateCartWithCacheAsync(CreateOrUpdateCartCommand command, Cart cart, CancellationToken cancellationToken)
    {
        await _repository.CreateOrUpdateCartAsync(cart, cancellationToken);

        var cartKey = new CartCacheSetOptions(command, TimeSpan.FromMinutes(2));
        await _cache.SetAsync(cartKey, cart);
    }
}
