using Ambev.DeveloperEvaluation.Application.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts._Shared;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    public CartsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost(Name = "CreateCart")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCart([FromBody] CreateOrUpdateCartRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        await ValidateAsync<CreateOrUpdateCartRequestValidator, CreateOrUpdateCartRequest>(request, cancellationToken);

        var command = _mapper.Map<CreateOrUpdateCartCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<CartResponse>(result);

        return Created("CreateCart", response);
    }

    [HttpGet("branch/{branchId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<CartResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart([FromRoute] GetCartByUserRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        await ValidateAsync<GetCartByUserRequestValidator, GetCartByUserRequest>(request, cancellationToken);
        

        var command = _mapper.Map<GetCartByUserCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        var result = _mapper.Map<CartResponse>(response);

        return Ok(result);
    }

    [HttpDelete("branch/{branchId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCart([FromRoute] DeleteCartRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        await ValidateAsync<DeleteCartRequestValidator, DeleteCartRequest>(request, cancellationToken);
        

        var command = _mapper.Map<DeleteCartCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

}
