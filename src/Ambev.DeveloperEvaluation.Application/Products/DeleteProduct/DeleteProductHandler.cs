using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductService _productService;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteProductHandler> _logger;

    public DeleteProductHandler(
        IProductService productService,
        ICommandValidatorExecutor validatorExecutor,
        IMapper mapper,
        ILogger<DeleteProductHandler> logger)
    {
        _productService = productService;
        _mapper = mapper;
        _logger = logger;
        _validatorExecutor = validatorExecutor;
    }
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[DeletProductHandler] Start");

        await _validatorExecutor.ValidateAsync<DeleteProductCommandValidator, DeleteProductCommand>(command, cancellationToken);

        await _productService.DeleteAsync(command.Id, cancellationToken);

        _logger.LogInformation("[DeletProductHandler] Finish");
    }
}
