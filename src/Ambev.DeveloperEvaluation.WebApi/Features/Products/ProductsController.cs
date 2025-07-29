using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Products._Shared;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateOrUpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    public ProductsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost(Name = "CreateOrUpdateProduct")]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrUpdateProduct([FromBody] CreateOrUpdateProductRequest request, CancellationToken cancellationToken)
    {
        var actionResult = await ValidateAsync<CreateOrUpdateProductRequestValidator, CreateOrUpdateProductRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<CreateOrUpdateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created("CreateOrUpdateProduct", request, response);
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var actionResult = await ValidateAsync<DeleteProductRequestValidator, DeleteProductRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<DeleteProductCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpGet(Name = "ListProducts")]
    [ProducesResponseType(typeof(ApiResponseWithData<IList<ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsRequest request, CancellationToken cancellationToken)
    {
        var actionResult = await ValidateAsync<ListProductsRequestValidator, ListProductsRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<ListProductsCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        var data = _mapper.Map<Paginated<ProductResponse>>(response);

        var paginatedList = new PaginatedList<ProductResponse>(data.Items, data.TotalCount, request.Page, request.PageSize);

        return OkPaginated(paginatedList);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(ApiResponseWithData<IList<ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] GetProductRequest request, CancellationToken cancellationToken)
    {
        var actionResult = await ValidateAsync<GetProductRequestValidator, GetProductRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<GetProductCommand>(request);

        var response = await _mediator.Send(command, cancellationToken);

        var result = _mapper.Map<ProductResult>(response);

        return Ok(result);
    }
}
