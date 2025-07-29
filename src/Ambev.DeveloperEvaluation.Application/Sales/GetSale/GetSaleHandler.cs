using Ambev.DeveloperEvaluation.Application.Sales._Shared;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleCommand, SaleResult>
{
    private readonly ISaleService _saleService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleHandler> _logger;

    public GetSaleHandler(
        ISaleService saleService,
        ICommandValidatorExecutor validatorExecutor,
        ILogger<GetSaleHandler> logger,
        IMapper mapper)
    {
        _saleService = saleService;
        _validatorExecutor = validatorExecutor;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<SaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetSaleHandler] Start - SaleId {SaleId}, SaleNumber {SaleNumber}", command.SaleId, command.SaleNumber);

        await _validatorExecutor.ValidateAsync<GetSaleCommandValidator, GetSaleCommand>(command, cancellationToken);

        var sale = await _saleService.GetAsync(command.SaleId, command.SaleNumber, cancellationToken);

        var result = _mapper.Map<SaleResult>(sale);

        _logger.LogInformation("[GetSaleHandler] Finish - SaleId {SaleId}, SaleNumber {SaleNumber}", command.SaleId, command.SaleNumber);

        return result;
    }
}
