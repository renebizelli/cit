using Ambev.DeveloperEvaluation.Application.Users._Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

public class UserServiceTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly UserService _service;

    public UserServiceTests()
    {
        _service = new UserService(_userRepository);
    }

    private static User CreateUser(string username = "john.doe", string email = "john@doe.com", UserRole role = UserRole.Customer, UserStatus status = UserStatus.Active)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            Role = role,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };
    }

    [Fact]
    public async Task ListUsersAsync_ReturnsUsers()
    {
        var querySettings = Substitute.For<IUserQuerySettings>();
        var users = new List<User>
        {
            CreateUser(),
            CreateUser("jane.doe", "jane@doe.com", UserRole.Manager, UserStatus.Inactive)
        };
        _userRepository.ListUsersAsync(querySettings, Arg.Any<Dictionary<string, System.Linq.Expressions.Expression<Func<User, object>>>>(), Arg.Any<CancellationToken>())
            .Returns((users.Count, users));

        var (count, result) = await _service.ListUsersAsync(querySettings, CancellationToken.None);

        count.Should().Be(users.Count);
        result.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task ListUsersAsync_ReturnsEmpty_WhenNoUsers()
    {
        var querySettings = Substitute.For<IUserQuerySettings>();
        _userRepository.ListUsersAsync(querySettings, Arg.Any<Dictionary<string, System.Linq.Expressions.Expression<Func<User, object>>>>(), Arg.Any<CancellationToken>())
            .Returns((0, new List<User>()));

        var (count, result) = await _service.ListUsersAsync(querySettings, CancellationToken.None);

        count.Should().Be(0);
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ListUsersAsync_PassesAllowedOrderFields()
    {
        var querySettings = Substitute.For<IUserQuerySettings>();
        var users = new List<User> { CreateUser() };
        Dictionary<string, System.Linq.Expressions.Expression<Func<User, object>>> capturedOrderFields = null;

        _userRepository.ListUsersAsync(querySettings, Arg.Do<Dictionary<string, System.Linq.Expressions.Expression<Func<User, object>>>>(d => capturedOrderFields = d), Arg.Any<CancellationToken>())
            .Returns((users.Count, users));

        await _service.ListUsersAsync(querySettings, CancellationToken.None);

        capturedOrderFields.Should().NotBeNull();
        capturedOrderFields.Keys.Should().Contain(new[] { "username", "email", "role", "createdAt" });
    }

    [Fact]
    public async Task ListUsersAsync_ThrowsException_WhenRepositoryThrows()
    {
        var querySettings = Substitute.For<IUserQuerySettings>();
        _userRepository.ListUsersAsync(querySettings, Arg.Any<Dictionary<string, System.Linq.Expressions.Expression<Func<User, object>>>>(), Arg.Any<CancellationToken>())
            .Throws(new Exception("Repository error"));

        Func<Task> act = async () => await _service.ListUsersAsync(querySettings, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Repository error");
    }
}