using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;

public class OrderSettings : IOrderSettings
{
    public List<(string, bool)> Criteria { get; set; } = new();
}
