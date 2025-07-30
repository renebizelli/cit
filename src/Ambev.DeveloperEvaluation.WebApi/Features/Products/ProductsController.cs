using Ambev.DeveloperEvaluation.Application.ProductRatings;
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
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Rating;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize (Roles = "Admin")]
    [HttpPost(Name = "CreateOrUpdateProduct")]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrUpdateProduct([FromBody] CreateOrUpdateProductRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync<CreateOrUpdateProductRequestValidator, CreateOrUpdateProductRequest>(request, cancellationToken);

        var command = _mapper.Map<CreateOrUpdateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<ProductResponse>(result);

        return Created("CreateOrUpdateProduct", request, response);
    }

    [Authorize (Roles = "Admin")]
    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync<DeleteProductRequestValidator, DeleteProductRequest>(request, cancellationToken);

        var command = _mapper.Map<DeleteProductCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [Authorize]
    [HttpGet(Name = "ListProducts")]
    [ProducesResponseType(typeof(ApiResponseWithData<IList<ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListProducts([FromQuery] ListProductsRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync<ListProductsRequestValidator, ListProductsRequest>(request, cancellationToken);

        var command = _mapper.Map<ListProductsCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var data = _mapper.Map<Paginated<ProductResponse>>(result);

        var paginatedList = new PaginatedList<ProductResponse>(data.Items, data.TotalCount, request.Page, request.PageSize);

        return OkPaginated(paginatedList);
    }

    [Authorize]
    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(typeof(ApiResponseWithData<IList<ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] GetProductRequest request, CancellationToken cancellationToken)
    {
        await ValidateAsync<GetProductRequestValidator, GetProductRequest>(request, cancellationToken);

        var command = _mapper.Map<GetProductCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<ProductResponse>(result);

        return Ok(response);
    }


    [Authorize]
    [HttpPost("{productId}/rating", Name = "SaveRating")]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveRating([FromRoute] string productId, [FromBody] ProductRatingRequest request, CancellationToken cancellationToken)
    {
        request.ProductId = productId;
        request.UserId = GetCurrentUserId();

        await ValidateAsync<ProductRatingRequestValidator, ProductRatingRequest>(request, cancellationToken);

        var command = _mapper.Map<ProductRatingCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent(); 
    }
}
