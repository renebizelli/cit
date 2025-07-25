namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
public class CreateProductResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CreateProductCategoryResponse Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public CreateProductRatingResponse? Rating { get; set; }
}

public class CreateProductCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}

public class CreateProductRatingResponse
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}