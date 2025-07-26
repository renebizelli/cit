using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductHandler : IRequestHandler<CreateOrUpdateProductCommand, CreateOrUpdateProductResult>
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

    public async Task<CreateOrUpdateProductResult> Handle(CreateOrUpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateOrUpdateProduct] Start - Title {Title}", command.Title);

        await _validatorExecutor.ValidateAsync<CreateOrUpdateProductCommandValidator, CreateOrUpdateProductCommand>(command, cancellationToken);

        await IsNameAlreadyUsedAsync(command.Title, cancellationToken);

        var product = _mapper.Map<Product>(command);

        await EnrichWithCategoryAsync(command.CategoryId, product, cancellationToken);

        var createdProduct = await _productService.CreateOrUpdateAsync(product, cancellationToken);

        var result = _mapper.Map<CreateOrUpdateProductResult>(createdProduct);

        _logger.LogInformation("[CreateOrUpdateProduct] Finish - Title {Title}", command.Title);

        return result;
    }

    private async Task  IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken)
    {
        var isNameAlreadyUsed = await _productService.IsNameAlreadyUsedAsync(title.Trim(), cancellationToken);
        if (isNameAlreadyUsed) throw new InvalidOperationException("##TODO isNameAlreadyUsed");
    }

    private async Task EnrichWithCategoryAsync(int categoryId, Product product, CancellationToken cancellationToken)
    {
        await _productService.EnrichWithCategoryAsync(categoryId, product, cancellationToken);
        
        if (product.Category == null) throw new InvalidOperationException("##TODO isNameAlreadyUsed");
    }






}
