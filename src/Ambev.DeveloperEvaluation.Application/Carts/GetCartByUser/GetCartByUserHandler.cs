using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserHandler : IRequestHandler<GetCartByUserCommand, GetCartByUserResult>
{
    private readonly ICacheRepository _cache;
    private readonly ICartRepository _repository;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartByUserHandler> _logger;

    public GetCartByUserHandler(
        ICacheRepository cache,
        ICartRepository repository,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<GetCartByUserHandler> logger)
    {
        _cache = cache;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    public async Task<GetCartByUserResult> Handle(GetCartByUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetCartByUser] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        await _validatorExecutor.ValidateAsync<GetCartByUserCommandValidator, GetCartByUserCommand>(command, cancellationToken);

        var cacheGetOptions = new CartCacheGetOptions(command);
        var cart = await GetCartAsync(command, cacheGetOptions, cancellationToken);

        var result = _mapper.Map<GetCartByUserResult>(cart);

        _logger.LogInformation("[GetCartByUser] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        return result;
    }

    private async Task<Cart> GetCartAsync(GetCartByUserCommand command, CartCacheGetOptions cacheGetOptions, CancellationToken cancellationToken)
    {
        var cart = await _cache.GetAsync<Cart>(cacheGetOptions);

        if (cart != null) return cart;

        cart = await GetOrCreateCartAsync(command, cart, cancellationToken);

        await SetCartInCacheAsync(command, cart);

        return cart;
    }

    private async Task<Cart> GetOrCreateCartAsync(GetCartByUserCommand command, Cart? cart, CancellationToken cancellationToken)
    {
        cart = await _repository.GetCartByUserAsync(command, cancellationToken);

        if (cart == null)
        {
            cart = _mapper.Map<Cart>(command);
            await _repository.CreateOrUpdateCartAsync(cart, cancellationToken);
        }

        return cart;
    }

    private async Task SetCartInCacheAsync(GetCartByUserCommand command, Cart cart)
    {
        var cacheSetOptions = new CartCacheSetOptions(command, TimeSpan.FromMinutes(2));
        await _cache.SetAsync<Cart>(cacheSetOptions, cart);
    }
}
