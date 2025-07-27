using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductHandler : IRequestHandler<CreateOrUpdateProductCommand, ProductResult>
{
    private readonly IProductService _productService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateProductHandler> _logger;

    public CreateOrUpdateProductHandler(
        IProductService productService,
        ICommandValidatorExecutor validatorExecutor, 
        IMapper mapper, 
        ILogger<CreateOrUpdateProductHandler> logger)
    {
        _productService = productService;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductResult> Handle(CreateOrUpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateOrUpdateProduct] Start - Title {Title}", command.Title);

        await _validatorExecutor.ValidateAsync<CreateOrUpdateProductCommandValidator, CreateOrUpdateProductCommand>(command, cancellationToken);

        var product = _mapper.Map<Product>(command);

        var createdProduct = await _productService.CreateOrUpdateAsync(product, cancellationToken);

        var result = _mapper.Map<ProductResult>(createdProduct);

        _logger.LogInformation("[CreateOrUpdateProduct] Finish - Title {Title}", command.Title);

        return result;
    }
}
