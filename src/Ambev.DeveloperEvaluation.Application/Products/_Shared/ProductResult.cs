namespace Ambev.DeveloperEvaluation.Application.Products._Shared;

public class ProductResult
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProductCategoryResult Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public ProductRatingResult Rating { get; set; } = new();
}

public class ProductCategoryResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ProductRatingResult
{
    public long Count { get; set; }
    public decimal Avg { get; set; }
}
