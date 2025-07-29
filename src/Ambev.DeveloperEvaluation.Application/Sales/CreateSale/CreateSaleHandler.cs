using Ambev.DeveloperEvaluation.Application.Sales._Shared;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
{
    private readonly ISaleService _saleService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;


    public CreateSaleHandler(
        ISaleService saleService,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleService = saleService;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateSale] Start - UserId {UserId}, BranchId {BranchId}", command.UserId, command.BranchId);

        await _validatorExecutor.ValidateAsync<CreateSaleCommandValidator, CreateSaleCommand>(command, cancellationToken);

        var sale = await _saleService.CreateAsync(command, cancellationToken);

        var result = _mapper.Map<SaleResult>(sale);

        _logger.LogInformation("[CreateSale] Finish - UserId {UserId}, BranchId {BranchId}", command.UserId, command.BranchId);

        return result;
    }
}
