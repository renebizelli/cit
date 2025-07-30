namespace Ambev.DeveloperEvaluation.WebApi.Features.Products._Shared;
public class ProductResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProductCategoryResponse Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public ProductRatingResponse? Rating { get; set; }
}

public class ProductCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}

public class ProductRatingResponse
{
    public long Count { get; set; }
    public decimal Avg { get; set; }
}