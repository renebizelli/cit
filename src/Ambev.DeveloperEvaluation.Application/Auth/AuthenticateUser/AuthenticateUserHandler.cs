using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IActiveUserSpecification _activeUserSpecification;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticateUserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IActiveUserSpecification activeUserSpecification,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _activeUserSpecification = activeUserSpecification;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (!_activeUserSpecification.IsSatisfiedBy(user))
            {
                throw new UnauthorizedAccessException("User is not active");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticateUserResult
            {
                Token = token,
                Email = user.Email,
                Name = user.Username,
                Role = user.Role.ToString()
            };
        }
    }
}
