using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Messages
{
    public class UserRegistered
    {
        public User User { get; }

        public UserRegistered(User user)
        {
            User = user;
        }
    }
}
