namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateOrUpdateProduct;
public class CreateOrUpdateProductResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CreateOrUpdateProductCategoryResponse Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public CreateOrUpdateProductRatingResponse? Rating { get; set; }
}

public class CreateOrUpdateProductCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}

public class CreateOrUpdateProductRatingResponse
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}