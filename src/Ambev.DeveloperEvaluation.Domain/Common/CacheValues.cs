namespace Ambev.DeveloperEvaluation.Domain.Common;

public abstract record CacheSetOptions : CacheGetOptions
{
    public TimeSpan Expiry { get; protected set; } = new();
    protected CacheSetOptions(string key, TimeSpan expiry) : base(key)
    {
        Expiry = expiry;
    }
}


public abstract record CacheGetOptions
{
    public string Key { get; protected set; } = string.Empty;

    protected CacheGetOptions(string key)
    {
        Key = key;
    }
}
