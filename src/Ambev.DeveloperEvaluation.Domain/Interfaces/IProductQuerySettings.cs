namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IProductQuerySettings : IListSettings
{
    string? Title { get; set; }
    string? Description { get; set; }
    decimal MinPrice { get; set; }
    decimal MaxPrice { get; set; }
}


