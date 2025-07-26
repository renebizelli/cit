namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IProductQuerySettings : IListSettings
{
    string? Category { get; set; }
    string? Title { get; set; }
    decimal MinPrice { get; set; }
    decimal MaxPrice { get; set; }
}


