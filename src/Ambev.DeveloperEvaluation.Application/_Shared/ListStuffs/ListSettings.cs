using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;

public class ListSettings : IListSettings
{
    public IOrderSettings? OrderSettings { get; set; }
    public IPagingSettings? PagingSettings { get; set; }
}
