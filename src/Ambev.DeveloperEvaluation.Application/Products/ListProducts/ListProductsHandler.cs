using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsCommand, PaginatedResult<ProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ListProductsHandler> _logger;

    public ListProductsHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<ListProductsHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<ProductResult>> Handle(ListProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ListProducts] Start");

        var paginationData = await _productRepository.ListProductsAsync(request, cancellationToken);

        _logger.LogInformation("[ListProducts] Finish");

        return _mapper.Map<PaginatedResult<ProductResult>>(paginationData);
    }
}
