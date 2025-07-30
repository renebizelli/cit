using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductCommand, ProductResult>
{
    private readonly IProductService _productService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductHandler> _logger;

    public GetProductHandler(
        IProductService productService,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<GetProductHandler> logger)
    {
        _productService = productService;
        _mapper = mapper;
        _logger = logger;
        _validatorExecutor = validatorExecutor;
    }
    public async Task<ProductResult> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetProductResult] Start");

        await _validatorExecutor.ValidateAsync<GetProductCommandValidator, GetProductCommand>(command, cancellationToken);

        var product = await _productService.GetAsync(command.Id, cancellationToken);

        var result = _mapper.Map<ProductResult>(product);

        _logger.LogInformation("[GetProductResult] Finish");

        return result;

    }
}
