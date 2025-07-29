namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IUserBranchKey
{
    Guid BranchId { get; set; }
    Guid UserId { get; set; }
}