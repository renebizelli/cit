using Ambev.DeveloperEvaluation.Application.Sales._Services;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
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

        //await _saleRepository.CreateSaleAsync(sale, cancellationToken);

        //await _cartRepository.DeleteCart(filter, cancellationToken);

        //await _bus.Send(new SaleCreatedEvent(sale.Id));

        //sale = await _saleRepository.GetAsync(sale.Id, cancellationToken);

        //var result = _mapper.Map<SaleResult>(sale);

        _logger.LogInformation("[CreateSale] Finish - UserId {UserId}, BranchId {BranchId}", command.UserId, command.BranchId);

        return null;
    }
}
