using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;

public class CreateOrUpdateCartHandler : IRequestHandler<CreateOrUpdateCartCommand>
{
    private readonly ICacheRepository _cacheRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateCartHandler> _logger;

    public CreateOrUpdateCartHandler(
        ICacheRepository cacheRepository,
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<CreateOrUpdateCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _cacheRepository = cacheRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(CreateOrUpdateCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateOrUpdateCart] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        var groupedItemsCommand = _mapper.Map<CreateOrUpdateCartCommand>(command);

        await ValidateAsync(command, cancellationToken);

        var cart = _mapper.Map<Cart>(groupedItemsCommand);

        await CreateOrUpdateCartWithCacheAsync(command, cart, cancellationToken);

        _logger.LogInformation("[CreateOrUpdateCart] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);
    }

    private async Task CreateOrUpdateCartWithCacheAsync(CreateOrUpdateCartCommand command, Cart cart, CancellationToken cancellationToken)
    {
        await _cartRepository.CreateOrUpdateCartAsync(cart, cancellationToken);

        var cartKey = new CartCacheSetOptions(command, TimeSpan.FromMinutes(2));
        await _cacheRepository.SetAsync(cartKey, cart);
    }

    private static async Task ValidateAsync(CreateOrUpdateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrUpdateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
