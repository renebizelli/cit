namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}
