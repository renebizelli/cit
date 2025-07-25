using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;


public class CreateOrUpdateCartCommandValidatorTest
{
    private CommandValidatorExecutor _validatorExecutor;

    public CreateOrUpdateCartCommandValidatorTest()
    {
        _validatorExecutor = new CommandValidatorExecutor();
    }

    [Fact(DisplayName = "Given a command with an empty items, it should throw a ValidationException.")]
    public async Task Empty_Items_Thrown_ValidationException()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();

        command.Items.Clear();

        var act = async () => await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a command with an empty branch, it should throw a ValidationException.")]
    public async Task Empty_Branch_Thrown_ValidationException()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();

        command.BranchId = Guid.Empty;

        var act = async () => await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a command with an empty user, it should throw a ValidationException.")]
    public async Task Empty_User_Thrown_ValidationException()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();

        command.UserId = Guid.Empty;

        var act = async () => await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }


    [Fact(DisplayName = "Given a command with a zero quantity, it should throw a ValidationException.")]
    public async Task Quantity_of_Item_Zero_Thrown_ValidationException()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();

        command.Items.First().Quantity = 0;

        var act = async () => await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a command with a exceeded quantity, it should throw a ValidationException.")]
    public async Task Quantity_of_Item_Exceeded_Thrown_ValidationException()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();

        command.Items.First().Quantity = 21;

        var act = async () => await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given a command with an invalid ID product, it should throw a ValidationException.")]
    public async Task Invalid_Product_Id_Thrown_ValidationException()
    {
        var command = CreateOrUpdateCartHandlerTestData.GenerateValidCommand();
        command.Items.First().ProductId = 0;

        var act = async () => await _validatorExecutor.ValidateAsync<CreateOrUpdateCartCommandValidator, CreateOrUpdateCartCommand>(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
