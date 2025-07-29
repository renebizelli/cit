using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
{
    private readonly ISaleService _saleService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CancelSaleHandler(
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

    public async Task Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CancelSale] Start - UserId {UserId}, SaleId {SaleId}, ProductId {ProductId}", command.UserId, command.Id, command.Id);

        await _validatorExecutor.ValidateAsync<CancelSaleCommandValidator, CancelSaleCommand>(command, cancellationToken);

        await _saleService.CancelAsync(command.Id, cancellationToken);

        _logger.LogInformation("[CancelSale] Finish - UserId {UserId}, SaleId {SaleId}, ProductId {ProductId}", command.UserId, command.Id, command.Id);
    }
}
