namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProductCategory? Category { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool Active { get; set; }
}

public class ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
