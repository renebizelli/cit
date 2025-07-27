using Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsRequest : ListSettingsRequest
{
    public int CategoryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }

}
