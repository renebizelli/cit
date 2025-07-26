namespace Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;

public class CreateOrUpdateProductResult 
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CreateOrUpdateProductCategoryResult Category { get; set; } = new();
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public CreateOrUpdateProductRatingResult? Rating { get; set; }
}

public class CreateOrUpdateProductCategoryResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateOrUpdateProductRatingResult
{
    public int Count { get; set; }
    public decimal Rate { get; set; }
}