using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Controllers.Requests;
using Product.Application.Commands.CreateProduct;
using Product.Application.Commands.UpdateProductPrice;
using Product.Application.DTOs;
using Product.Application.Queries.GetAllProducts;
using Product.Application.Queries.GetProductById;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(
            new CreateProductCommand(
                request.Name,
                request.Description,
                request.Price,
                request.Currency ?? "IRR",
                request.StockQuantity),
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPut("{id:guid}/price")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePrice(
        Guid id,
        [FromBody] UpdateProductPriceRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(
                new UpdateProductPriceCommand(id, request.NewPrice, request.Currency ?? "IRR"),
                cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
