namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class ProductRating
{
    public string ProductId { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
