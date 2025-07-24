using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

internal class GetCartsByUserHandler : IRequestHandler<GetCartByUserCommand, GetCartByUserResult>
{
    private readonly ICacheRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateCartHandler> _logger;

    public GetCartsByUserHandler(
        ICacheRepository cartRepository,
        IMapper mapper,
        ILogger<CreateOrUpdateCartHandler> logger)
    {
        _repository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetCartByUserResult> Handle(GetCartByUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetCartByUser] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        var cacheGetOptions = new CartCacheGetOptions(command);

        var cart = await _repository.GetAsync<Cart>(cacheGetOptions);

        var result = _mapper.Map<GetCartByUserResult>(cart);

        _logger.LogInformation("[GetCartByUser] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        return result;
    }
}
