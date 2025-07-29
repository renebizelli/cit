namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IStringIDGenerator
{
    public string Generate();
}

public interface ILongIDGenerator
{
    Task<long> GenerateSaleNumber(CancellationToken cancellationToken);
}
