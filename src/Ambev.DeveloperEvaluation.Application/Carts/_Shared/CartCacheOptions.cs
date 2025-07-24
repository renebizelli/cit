using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts._Shared;

internal static class CartKeyCommandExtension
{
    public static string KeyBuilder(this CartKey cartKey) => $"{cartKey.UserId}_{cartKey.BranchId}";
}

internal record CartCacheSetOptions : CacheSetOptions
{
    public CartCacheSetOptions(CartKey cartKey, TimeSpan expiry) : base(cartKey.KeyBuilder(), expiry) { }
}

internal record CartCacheGetOptions : CacheGetOptions
{
    public CartCacheGetOptions(CartKey cartKey) : base(cartKey.KeyBuilder()) { }
}
