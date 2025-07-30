namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Rating;

public class ProductRatingRequest
{
    public string ProductId { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public int Rating { get; set; }
}
