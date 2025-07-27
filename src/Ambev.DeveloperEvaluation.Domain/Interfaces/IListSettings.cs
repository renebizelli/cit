namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IListSettings
{
    IOrderSettings? OrderSettings { get; set; }
    IPagingSettings? PagingSettings { get; set; }
}

public interface IOrderSettings
{
    List<(string, bool)> Criteria { get; set; }
}

public interface IPagingSettings
{
    int Page { get; set; }
    int Size { get; set; }
}