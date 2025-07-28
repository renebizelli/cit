using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    public SalesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost(Name = "CreateSale")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        var actionResult = await ValidateAsync<CreateSaleRequestValidator, CreateSaleRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<CreateSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<SaleResponse>(result);

        return Ok(response);
    }

    //[Authorize(Roles = "Admin")]
    //[HttpGet("{saleId}")]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetSale([FromRoute] GetSaleRequest request, CancellationToken cancellationToken)
    //{
    //    var validator = new GetSaleRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<GetSaleCommand>(request);

    //    var result = await _mediator.Send(command, cancellationToken);

    //    var response = _mapper.Map<SaleDetailResponse>(result);

    //    return Ok(response);
    //}

    //[Authorize(Roles = "Admin")]
    //[HttpGet()]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> ListSale([FromQuery] ListSalesRequest request,  CancellationToken cancellationToken)
    //{
    //    var validator = new ListSalesRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<ListSalesCommand>(request);

    //    var result = await _mediator.Send(command, cancellationToken);

    //    var response = _mapper.Map<Paginated<SaleResponse>>(result);

    //    return Ok(response);
    //}

    //[Authorize(Roles = "Admin")]
    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> CancelSale([FromRoute] CancelSaleRequest request, CancellationToken cancellationToken)
    //{
    //    var validator = new CancelSaleRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<CancelSaleCommand>(request);

    //    var result = await _mediator.Send(command, cancellationToken);

    //    var response = _mapper.Map<CancelSaleResponse>(result);

    //    return Ok(response);
    //}

    //[Authorize(Roles = "Admin")]
    //[HttpDelete("{saleId}/items/{saleItemId}")]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> CancelSaleItem([FromRoute] CancelSaleItemRequest request, CancellationToken cancellationToken)
    //{
    //    var validator = new CancelSaleItemRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<CancelSaleItemCommand>(request);

    //    var result = await _mediator.Send(command, cancellationToken);

    //    var response = _mapper.Map<CancelSaleItemResponse>(result);

    //    return Ok(response);
    //}

}
