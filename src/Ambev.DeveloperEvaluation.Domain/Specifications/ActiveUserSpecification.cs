using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface IActiveUserSpecification : ISpecification<User>;

public class ActiveUserSpecification :  IActiveUserSpecification
{
    public bool IsSatisfiedBy(User user)
    {
        if (user == null) return false;

        return user.Status == UserStatus.Active;
    }
}
