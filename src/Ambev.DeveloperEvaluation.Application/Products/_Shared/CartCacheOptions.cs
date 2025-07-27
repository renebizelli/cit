using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Products._Shared;

public static class ProductKeyCommandExtension
{
    public static string KeyBuilder(this string productId) => $"product:{productId}";
}

public record ProductCacheSetOptions : CacheSetOptions
{
    public ProductCacheSetOptions(string productId, TimeSpan expiry) : base(productId.KeyBuilder(), expiry) { }
}

public record ProductCacheGetOptions : CacheGetOptions
{
    public ProductCacheGetOptions(string productId) : base(productId.KeyBuilder()) { }
}

public record ProductCacheDeleteOptions : CacheDeleteOptions
{
    public ProductCacheDeleteOptions(string productId) : base(productId.KeyBuilder()) { }
}
