namespace Ambev.DeveloperEvaluation.NoSQL.MongoDB.Models;

public class Counter
{
    public string Id { get; set; } = string.Empty;
    public long Sequence { get; set; }
}
