using Ambev.DeveloperEvaluation.Application.Carts._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetCartByUserHandlerTest
{
    private readonly ICartRepository _repository;
    private readonly ICacheRepository _cache;
    private readonly ICommandValidatorExecutor _validatorExecutor;

    private readonly IMapper _mapper;
    private readonly ILogger<GetCartByUserHandler> _logger;
    private readonly GetCartByUserHandler _handler;

    public GetCartByUserHandlerTest()
    {
        _cache = Substitute.For<ICacheRepository>();
        _repository = Substitute.For<ICartRepository>();
        _validatorExecutor = Substitute.For<ICommandValidatorExecutor>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetCartByUserHandler>>();
        _handler = new GetCartByUserHandler(_cache, _repository, _validatorExecutor, _mapper, _logger);
    }

    [Fact(DisplayName = "Given a valid command, when retrieving a cart from the cache, then it should return the cart.")]
    public async Task Handle_Retrieving_Cart_From_Cache()
    {
        var command = GetCartByUserHandlerTestData.GenerateValidCommand();

        var cartCacheGetOptions = new CartCacheGetOptions(command);

        _mapper.Map<CartCacheGetOptions>(command).Returns(cartCacheGetOptions);

        _cache.GetAsync<Cart>(Arg.Any<CartCacheGetOptions>()).Returns(new Cart());

        await _handler.Handle(command, CancellationToken.None);

        await _validatorExecutor.Received(1).ValidateAsync<GetCartByUserCommandValidator, GetCartByUserCommand>(Arg.Any<GetCartByUserCommand>(), Arg.Any<CancellationToken>());

        await _cache.Received(1).GetAsync<Cart>(Arg.Any<CartCacheGetOptions>());
    }

    [Fact(DisplayName = "Given a valid command, when the cart is not found in the cache, then it should be retrieved from the database.")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        var command = GetCartByUserHandlerTestData.GenerateValidCommand();

        var cartCacheGetOptions = new CartCacheGetOptions(command);

        _mapper.Map<CartCacheGetOptions>(command).Returns(cartCacheGetOptions);

        _cache.GetAsync<Cart>(Arg.Any<CartCacheGetOptions>()).ReturnsNull();
        _repository.GetCartByUserAsync(Arg.Any<CartKey>()).Returns(new Cart());

        await _handler.Handle(command, CancellationToken.None);

        await _validatorExecutor.Received(1).ValidateAsync<GetCartByUserCommandValidator, GetCartByUserCommand>(Arg.Any<GetCartByUserCommand>(), Arg.Any<CancellationToken>());

        await _repository.Received(1).GetCartByUserAsync(Arg.Any<CartKey>(), Arg.Any<CancellationToken>());
    }
}
