using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateOrUpdateCartHandlerTest
{
    private readonly ICartRepository _cartRepository;
    private readonly ICacheRepository _cacheRepository;
    private readonly ICommandValidatorExecutor _validatorExecutor;

    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdateCartHandler> _logger;
    private readonly CreateOrUpdateCartHandler _handler;

    public CreateOrUpdateCartHandlerTest()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _cacheRepository = Substitute.For<ICacheRepository>();
        _validatorExecutor = Substitute.For<ICommandValidatorExecutor>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateOrUpdateCartHandler>>();
        _handler = new CreateOrUpdateCartHandler(_cacheRepository, _cartRepository, _validatorExecutor, _mapper, _logger);
    }


    [Fact(DisplayName = "Given valid cart data When creating cart Then returns success")]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();
        var cart = new Cart
        {
            UserId = command.UserId,
            BranchId = command.BranchId,
            Items = command.Items.Select(s => new CartItem { ProductId = s.ProductId, Quantity = s.Quantity }).ToList()
        };

        _mapper.Map<Cart>(command).Returns(cart);

        await _handler.Handle(command, CancellationToken.None);

        await _validatorExecutor.Received(1).ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(Arg.Any<CreateOrUpdateCartCommand>(), Arg.Any<CancellationToken>());

        await _cartRepository.Received(1).CreateOrUpdateCartAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());

        await _cacheRepository.Received(1).SetAsync(Arg.Any<CacheSetOptions>(), Arg.Any<Cart>());
    }

    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new CreateOrUpdateCartCommand();

        _mapper.Map<CreateOrUpdateCartCommand>(command).Returns(command);

        _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(
              Arg.Any<CreateOrUpdateCartCommand>(), Arg.Any<CancellationToken>())
              .ThrowsAsync(new ValidationException(""));

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

}
