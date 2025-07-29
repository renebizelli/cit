using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface IUserQuerySettings : IListSettings
{
    string Username { get; set; }
    string Email { get; set; }
    UserRole Role { get; set; }
    UserStatus Status { get; set; }
    DateTime MinDate { get; set; }
    DateTime MaxDate { get; set; }
}
