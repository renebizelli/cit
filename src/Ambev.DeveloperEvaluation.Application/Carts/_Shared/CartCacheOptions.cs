using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts._Shared;

public static class CartKeyCommandExtension
{
    public static string KeyBuilder(this CartKey cartKey) => $"cart:{cartKey.UserId}_{cartKey.BranchId}";
}

public record CartCacheSetOptions : CacheSetOptions
{
    public CartCacheSetOptions(CartKey cartKey, TimeSpan expiry) : base(cartKey.KeyBuilder(), expiry) { }
}

public record CartCacheGetOptions : CacheGetOptions
{
    public CartCacheGetOptions(CartKey cartKey) : base(cartKey.KeyBuilder()) { }
}

public record CartCacheDeleteOptions : CacheDeleteOptions
{
    public CartCacheDeleteOptions(CartKey cartKey) : base(cartKey.KeyBuilder()) { }
}
