namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductResult 
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CreateProductCategoryResult Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public CreateProductRatingResult? Rating { get; set; }
}

public class CreateProductCategoryResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateProductRatingResult
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}