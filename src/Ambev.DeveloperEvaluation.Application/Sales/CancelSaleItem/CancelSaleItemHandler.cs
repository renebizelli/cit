using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand>
{
    private readonly ISaleService _saleService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CancelSaleItemHandler(
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

    public async Task Handle(CancelSaleItemCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CancelSaleItem] Start - UserId {UserId}, SaleId {SaleId}, ProductId {ProductId}", command.UserId, command.SaleId, command.SaleItemId);

        await _validatorExecutor.ValidateAsync<CancelSaleItemCommandValidator, CancelSaleItemCommand>(command, cancellationToken);

        await _saleService.CancelSaleItemAsync(command.SaleId, command.SaleItemId, cancellationToken);

        _logger.LogInformation("[CancelSaleItem] Finish - SaleId {SaleId}, ProductId {ProductId}", command.SaleId, command.SaleItemId);
    }
}
