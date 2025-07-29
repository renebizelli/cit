using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Application.Carts._Shared;

public static class CartKeyCommandExtension
{
    public static string KeyBuilder(this IUserBranchKey cartKey) => $"cart:{cartKey.UserId}_{cartKey.BranchId}";
}

public record CartCacheSetOptions : CacheSetOptions
{
    public CartCacheSetOptions(IUserBranchKey cartKey, TimeSpan expiry) : base(cartKey.KeyBuilder(), expiry) { }
}

public record CartCacheGetOptions : CacheGetOptions
{
    public CartCacheGetOptions(IUserBranchKey cartKey) : base(cartKey.KeyBuilder()) { }
}

public record CartCacheDeleteOptions : CacheDeleteOptions
{
    public CartCacheDeleteOptions(IUserBranchKey cartKey) : base(cartKey.KeyBuilder()) { }
}
