using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
{
    private readonly ICacheRepository _cache;
    private readonly ICartRepository _repository;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCartHandler> _logger;

    public DeleteCartHandler(
        ICacheRepository cache,
        ICartRepository repository,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<DeleteCartHandler> logger)
    {
        _cache = cache;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }
    public async Task Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[DeleteCart] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        await _validatorExecutor.ValidateAsync<DeleteCartCommandValidator, DeleteCartCommand>(command, cancellationToken);

        var sucess = await _repository.DeleteCartAsync(command, cancellationToken);

        if (!sucess) throw new KeyNotFoundException($"Cart not found");

        var cacheDeleteOptions = new CartCacheDeleteOptions(command);

        await _cache.DeleteAsync(cacheDeleteOptions);

        _logger.LogInformation("[DeleteCart] Finsh - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);
    }
}
