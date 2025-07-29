using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Sales._Shared;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesCommand, PaginatedResult<SaleResult>>
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;
    private readonly ILogger<ListSalesHandler> _logger;

    public ListSalesHandler(
        ISaleService saleService,
        IMapper mapper,
        ILogger<ListSalesHandler> logger)
    {
        _saleService = saleService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<SaleResult>> Handle(ListSalesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ListSales] Start");

        var paginationData = await _saleService.ListSalesAsync(request, cancellationToken);

        _logger.LogInformation("[ListSales] Finish");

        return _mapper.Map<PaginatedResult<SaleResult>>(paginationData);
    }
}
