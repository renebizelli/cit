namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ProductResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public ProductCategoryResponse Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}

public class ProductCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateProductRatingResponse
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}