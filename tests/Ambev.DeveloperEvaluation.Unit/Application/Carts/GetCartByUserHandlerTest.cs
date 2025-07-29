using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetCartByUserHandlerTest
{
    private readonly ICartService _service;
    private readonly ICommandValidatorExecutor _validatorExecutor;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartByUserHandler> _logger;
    private readonly GetCartByUserHandler _handler;

    public GetCartByUserHandlerTest()
    {
        _service = Substitute.For<ICartService>();
        _validatorExecutor = Substitute.For<ICommandValidatorExecutor>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetCartByUserHandler>>();
        _handler = new GetCartByUserHandler(_service, _validatorExecutor, _mapper, _logger);
    }

    [Fact(DisplayName = "Service throws exception when getting cart by user")]
    public async Task Handle_ServiceThrowsException_ShouldPropagate()
    {
        var command = GetCartByUserHandlerTestData.GenerateValidCommand();
        _service.GetByUserAsync(Arg.Any<UserBranchKey>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception("Service error"));

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>().WithMessage("Service error");
    }
}
