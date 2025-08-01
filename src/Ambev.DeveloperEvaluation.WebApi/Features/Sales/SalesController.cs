﻿using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    public SalesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [Authorize]
    [HttpPost(Name = "CreateSale")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        await ValidateAsync<CreateSaleRequestValidator, CreateSaleRequest>(request, cancellationToken);
        

        var command = _mapper.Map<CreateSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<SaleDetailResponse>(result);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("{value}", Name = "GetSaleRequest")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] GetSaleRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync<GetSaleRequestValidator, GetSaleRequest>(request, cancellationToken);
        

        var command = _mapper.Map<GetSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<SaleDetailResponse>(result);

        return Ok(response);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet(Name = "ListSales")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListSales([FromQuery] ListSalesRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync<ListSalesRequestValidator, ListSalesRequest>(request, cancellationToken);
        

        var command = _mapper.Map<ListSalesCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<Paginated<SaleResponse>>(result);

        var paginatedList = new PaginatedList<SaleResponse>(response.Items, response.TotalCount, request.Page, request.PageSize);

        return OkPaginated(paginatedList);
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromRoute] CancelSaleRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        await ValidateAsync<CancelSaleRequestValidator, CancelSaleRequest>(request, cancellationToken);
        

        var command = _mapper.Map<CancelSaleCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{saleId}/items/{saleItemId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSaleItem([FromRoute] CancelSaleItemRequest request, CancellationToken cancellationToken)
    {
        request.UserId = GetCurrentUserId();

        await ValidateAsync<CancelSaleItemRequestValidator, CancelSaleItemRequest>(request, cancellationToken);
        

        var command = _mapper.Map<CancelSaleItemCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

}
