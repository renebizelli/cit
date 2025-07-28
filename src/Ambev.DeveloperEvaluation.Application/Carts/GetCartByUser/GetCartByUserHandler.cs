using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

public class GetCartByUserHandler : IRequestHandler<GetCartByUserCommand, GetCartByUserResult>
{
    private readonly ICartService _cartService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartByUserHandler> _logger;

    public GetCartByUserHandler(
        ICartService cartService,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<GetCartByUserHandler> logger)
    {
        _cartService = cartService;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetCartByUserResult> Handle(GetCartByUserCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetCartByUser] Start - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        await _validatorExecutor.ValidateAsync<GetCartByUserCommandValidator, GetCartByUserCommand>(command, cancellationToken);

        var cart = await _cartService.GetByUserAsync(command, cancellationToken);

        var result = _mapper.Map<GetCartByUserResult>(cart);

        _logger.LogInformation("[GetCartByUser] Finish - UserId {UserId}, BranchId, {BranchId}", command.UserId, command.BranchId);

        return result;
    }

}
