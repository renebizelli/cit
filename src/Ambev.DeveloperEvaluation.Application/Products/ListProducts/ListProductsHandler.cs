using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsCommand, PaginatedResult<ProductResult>>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly ILogger<ListProductsHandler> _logger;

    public ListProductsHandler(
        IProductService productService,
        IMapper mapper,
        ILogger<ListProductsHandler> logger)
    {
        _productService = productService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<ProductResult>> Handle(ListProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ListProducts] Start");

        var paginationData = await _productService.ListProductsAsync(request, cancellationToken);

        _logger.LogInformation("[ListProducts] Finish");

        return _mapper.Map<PaginatedResult<ProductResult>>(paginationData);
    }
}
