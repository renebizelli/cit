using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Categories._Shared;

public static class CategoryKeyCommandExtension
{
    public static string KeyBuilder(this int categoryId) => $"category:{categoryId}";
}

public record CategoryCacheSetOptions : CacheSetOptions
{
    public CategoryCacheSetOptions(int categoryId, TimeSpan expiry) : base(categoryId.KeyBuilder(), expiry) { }
}

public record CategoryCacheGetOptions : CacheGetOptions
{
    public CategoryCacheGetOptions(int categoryId) : base(categoryId.KeyBuilder()) { }
}

public record CategoryCacheDeleteOptions : CacheDeleteOptions
{
    public CategoryCacheDeleteOptions(int categoryId) : base(categoryId.KeyBuilder()) { }
}
