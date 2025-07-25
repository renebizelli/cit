using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost()]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCart([FromBody] CreateOrUpdateCartRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        var actionResult = await ValidateAsync<CreateOrUpdateCartRequestValidator, CreateOrUpdateCartRequest>(request, cancellationToken);
        if(actionResult != null) return actionResult;

        var command = _mapper.Map<CreateOrUpdateCartCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return Created();
    }

    [HttpGet("branch/{branchId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartByUserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart([FromRoute] GetCartByUserRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        var actionResult = await ValidateAsync<GetCartByUserRequestValidator, GetCartByUserRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<GetCartByUserCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        var result = _mapper.Map<GetCartByUserResponse>(response);

        return Ok(result);
    }

    [HttpDelete("branch/{branchId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCart([FromRoute] DeleteCartRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        var actionResult = await ValidateAsync<DeleteCartRequestValidator, DeleteCartRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<DeleteCartCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

}
