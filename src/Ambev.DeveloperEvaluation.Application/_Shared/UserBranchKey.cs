using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.Application._Shared;

public class UserBranchKey : IUserBranchKey
{
    public UserBranchKey()
    {
            
    }

    public UserBranchKey(Guid userId, Guid branchId)
    {
        UserId = userId;
        BranchId = branchId;
    }

    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
}
 