using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Branches._Shared;

public static class BranchKeyCommandExtension
{
    public static string KeyBuilder(this Guid id) => $"category:{id}";
}

public record BranchCacheSetOptions : CacheSetOptions
{
    public BranchCacheSetOptions(Guid id, TimeSpan expiry) : base(id.KeyBuilder(), expiry) { }
}

public record BranchCacheGetOptions : CacheGetOptions
{
    public BranchCacheGetOptions(Guid id) : base(id.KeyBuilder()) { }
}

public record BranchCacheDeleteOptions : CacheDeleteOptions
{
    public BranchCacheDeleteOptions(Guid id) : base(id.KeyBuilder()) { }
}
