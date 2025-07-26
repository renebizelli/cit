namespace Ambev.DeveloperEvaluation.WebApi.Common.ListStuffs;

public class ListSettingsRequest
{
    public string? _Order { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
