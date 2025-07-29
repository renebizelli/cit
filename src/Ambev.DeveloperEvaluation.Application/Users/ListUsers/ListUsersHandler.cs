using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Users._Shared;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;


public class ListUsersHandler : IRequestHandler<ListUsersCommand, PaginatedResult<UserResult>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<ListUsersHandler> _logger;

    public ListUsersHandler(
        IUserService userService,
        IMapper mapper,
        ILogger<ListUsersHandler> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<UserResult>> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ListUsers] Start");

        var paginationData = await _userService.ListUsersAsync(command, cancellationToken);

        _logger.LogInformation("[ListUsers] Finish");

        return _mapper.Map<PaginatedResult<UserResult>>(paginationData);
    }
}
