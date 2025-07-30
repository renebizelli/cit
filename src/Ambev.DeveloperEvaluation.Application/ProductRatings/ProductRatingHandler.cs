using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.ProductRatings;

public class ProductRatingHandler : IRequestHandler<ProductRatingCommand>
{
    private readonly IProductRatingService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductRatingHandler> _logger;
    private readonly ICommandValidatorExecutor _validatorExecutor;

    public ProductRatingHandler(IProductRatingService service, IMapper mapper, ILogger<ProductRatingHandler> logger, ICommandValidatorExecutor validatorExecutor)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
        _validatorExecutor = validatorExecutor;
    }

    public async Task Handle(ProductRatingCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ProductRating] Start - ProductId {ProductId}, UserId {UserId}", command.ProductId, command.UserId);

        await _validatorExecutor.ValidateAsync<ProductRatingCommandValidator, ProductRatingCommand>(command, cancellationToken);
        
        var rating = _mapper.Map<Domain.Entities.ProductRating>(command);

        await _service.SaveAsync(rating, cancellationToken);

        _logger.LogInformation("[ProductRating] Finish - ProductId {ProductId}, UserId {UserId}", command.ProductId, command.UserId);

    }
}
