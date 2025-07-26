using Ambev.DeveloperEvaluation.Application.Products.CreateOrUpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateOrUpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateOrUpdateProduct;
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
    [ProducesResponseType(typeof(ApiResponseWithData<CreateOrUpdateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateOrUpdateProductResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrUpdateProduct([FromBody] CreateOrUpdateProductRequest request, CancellationToken cancellationToken)
    {
        var actionResult = await ValidateAsync<CreateOrUpdateProductRequestValidator, CreateOrUpdateProductRequest>(request, cancellationToken);
        if (actionResult != null) return actionResult;

        var command = _mapper.Map<CreateOrUpdateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created("CreateOrUpdateProduct", request, response);
    }

    //[HttpPut("{id}")]
    //[ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    //{
    //    request.Id = id;

    //    var validator = new UpdateProductRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<UpdateProductCommand>(request);
    //    var response = await _mediator.Send(command, cancellationToken);

    //    var data = _mapper.Map<UpdateProductResponse>(response);

    //    return Ok(data);
    //}

    //[HttpDelete("{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductRequest request, CancellationToken cancellationToken)
    //{
    //    var validator = new DeleteProductRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<DeleteProductCommand>(request);

    //    var result = await _mediator.Send(command, cancellationToken);

    //    var response = _mapper.Map<DeleteProductResponse>(result);

    //    return Ok(response);
    //}

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
}
