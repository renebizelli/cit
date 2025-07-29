using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class DeleteCartHandlerTest
{
    private readonly ICartService _service;
    private readonly ICommandValidatorExecutor _validatorExecutor;

    private readonly ILogger<DeleteCartHandler> _logger;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTest()
    {
        _service = Substitute.For<ICartService>();
        _validatorExecutor = Substitute.For<ICommandValidatorExecutor>();
        _logger = Substitute.For<ILogger<DeleteCartHandler>>();
        _handler = new DeleteCartHandler(_service, _validatorExecutor, _logger);
    }

    [Fact(DisplayName = "Given a valid command, when deleting a cart, then a success result should be returned.")]
    public async Task Handle_Detele_Cart()
    {
        var command = DeleteCartHandlerTestData.GenerateValidCommand();

        var cartCacheDeleteOptions = new CartCacheDeleteOptions(command);

        await _handler.Handle(command, CancellationToken.None);

        await _validatorExecutor.Received(1).ValidateAsync<DeleteCartCommandValidator, DeleteCartCommand>(Arg.Any<DeleteCartCommand>(), Arg.Any<CancellationToken>());

        await _service.Received(1).DeleteAsync(Arg.Any<UserBranchKey>(), Arg.Any<CancellationToken>());
    }

    //[Fact(DisplayName = "Given a valid command, when deleting a non-existent cart, then it should throw a KeyNotFoundException.")]
    //public async Task Handle_Detele_Non_Existent_Cart()
    //{
    //    var command = DeleteCartHandlerTestData.GenerateValidCommand();

    //    var cartCacheDeleteOptions = new CartCacheDeleteOptions(command);

    //    _service.DeleteAsync(Arg.Any<UserBranchKey>(), Arg.Any<CancellationToken>()).Returns(false);

    //    var act = async () => await _handler.Handle(command, CancellationToken.None);

    //    await act.Should().ThrowAsync<KeyNotFoundException>();

    //}
    
    [Fact(DisplayName = "Service throws exception when deleting cart")]
    public async Task Handle_ServiceThrowsException_ShouldPropagate()
    {
        var command = DeleteCartHandlerTestData.GenerateValidCommand();
        _service.DeleteAsync(Arg.Any<UserBranchKey>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception("Service error"));

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>().WithMessage("Service error");
    }
}
