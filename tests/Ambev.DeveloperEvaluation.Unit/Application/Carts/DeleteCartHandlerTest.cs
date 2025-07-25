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
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class DeleteCartHandlerTest
{
    private readonly ICartRepository _repository;
    private readonly ICacheRepository _cache;
    private readonly ICommandValidatorExecutor _validatorExecutor;

    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCartHandler> _logger;
    private readonly DeleteCartHandler _handler;

    public DeleteCartHandlerTest()
    {
        _cache = Substitute.For<ICacheRepository>();
        _repository = Substitute.For<ICartRepository>();
        _validatorExecutor = Substitute.For<ICommandValidatorExecutor>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<DeleteCartHandler>>();
        _handler = new DeleteCartHandler(_cache, _repository, _validatorExecutor, _mapper, _logger);
    }

    [Fact(DisplayName = "Given a valid command, when deleting a cart, then a success result should be returned.")]
    public async Task Handle_Detele_Cart()
    {
        var command = DeleteCartHandlerTestData.GenerateValidCommand();

        var cartCacheDeleteOptions = new CartCacheDeleteOptions(command);

        _mapper.Map<CartCacheDeleteOptions>(command).Returns(cartCacheDeleteOptions);

        _repository.DeleteCartAsync(Arg.Any<CartKey>(), Arg.Any<CancellationToken>()).Returns(true);

        await _handler.Handle(command, CancellationToken.None);

        await _validatorExecutor.Received(1).ValidateAsync<DeleteCartCommandValidator, DeleteCartCommand>(Arg.Any<DeleteCartCommand>(), Arg.Any<CancellationToken>());

        await _cache.Received(1).DeleteAsync(Arg.Any<CartCacheDeleteOptions>());
    }

    [Fact(DisplayName = "Given a valid command, when deleting a non-existent cart, then it should throw a KeyNotFoundException.")]
    public async Task Handle_Detele_Non_Existent_Cart()
    {
        var command = DeleteCartHandlerTestData.GenerateValidCommand();

        var cartCacheDeleteOptions = new CartCacheDeleteOptions(command);

        _mapper.Map<CartCacheDeleteOptions>(command).Returns(cartCacheDeleteOptions);

        _repository.DeleteCartAsync(Arg.Any<CartKey>(), Arg.Any<CancellationToken>()).Returns(false);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();

    }
}
