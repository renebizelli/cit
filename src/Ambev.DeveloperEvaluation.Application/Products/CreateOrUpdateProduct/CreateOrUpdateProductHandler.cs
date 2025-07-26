using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductHandler : IRequestHandler<CreateOrUpdateProductCommand, CreateOrUpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateProductHandler> _logger;

    public CreateOrUpdateProductHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        ICommandValidatorExecutor validatorExecutor, 
        IMapper mapper, 
        ILogger<CreateOrUpdateProductHandler> logger)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _validatorExecutor = validatorExecutor;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateOrUpdateProductResult> Handle(CreateOrUpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateOrUpdateProduct] Start - Title {Title}", command.Title);

        await _validatorExecutor.ValidateAsync<CreateOrUpdateProductCommandValidator, CreateOrUpdateProductCommand>(command, cancellationToken);

        await IsNameAlreadyUsedAsync(command.Title, cancellationToken);

        var product = await ProductMappingAsync(command, cancellationToken);

        var createdProduct = await _productRepository.CreateOrUpdateAsync(product, cancellationToken);

        var result = _mapper.Map<CreateOrUpdateProductResult>(createdProduct);

        _logger.LogInformation("[CreateOrUpdateProduct] Finish - Title {Title}", command.Title);

        return result;
    }

    private async Task  IsNameAlreadyUsedAsync(string title, CancellationToken cancellationToken)
    {
        var isNameAlreadyUsed = await _productRepository.IsNameAlreadyUsedAsync(title.Trim(), cancellationToken);
        if (isNameAlreadyUsed) throw new InvalidOperationException("##TODO isNameAlreadyUsed");
    }

    private async Task<Product> ProductMappingAsync(CreateOrUpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(command);

        var category = await _categoryRepository.GetAsync(command.CategoryId, cancellationToken);

        if (category == null) throw new InvalidOperationException("##TODO category not found");

        product.Category = category;

        return product;
    }
}
