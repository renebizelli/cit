using MediatR;

namespace Ambev.DeveloperEvaluation.Application.ProductRatings;

public class ProductRatingCommand : IRequest
{
    public string ProductId { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public int Rating { get; set; }
}
