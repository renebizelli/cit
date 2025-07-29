using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Branch : BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
}
