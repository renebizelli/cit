namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ProductResult
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProductCategoryResult Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
}


public class ProductCategoryResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}