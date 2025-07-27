using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Application._Shared.ListStuffs;

public class PagingSettings : IPagingSettings
{
    public int Page { get;set; }
    public int Size { get;set; }
}
