using Catalog.Application.Commands.Products;
using Catalog.Application.Queries.Brands;
using Catalog.Application.Queries.Products;
using Catalog.Application.Queries.Types;
using Catalog.Application.Responses;
using Catalog.Core.CatalogSpecs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class CatalogController(IMediator mediator, ILogger<CatalogController> logger) : ApiController
{
    //IActionResult => no output
    //ActionResult => output 
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetProductByIdQuery(id), cancellationToken));
    }

    //localhost:5152/api/v1/catalog/GetProductsByName/{name}
    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByName(string name,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetProductsByNameQuery(name), cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts(
        [FromQuery] GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        logger.LogInformation("CatalogController GetAllProducts GetAllProductsQuery {resultCount}", result.Count);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetAllBrands(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductBrandsQuery(), cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetAllTypes(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllProductsTypeQuery(), cancellationToken));
    }

    [HttpGet("{brand}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByBrandName(string brand,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsByBrandQuery(brand), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{type}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByTypeName(string type,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetProductsByTypeQuery(type), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new DeleteProductCommand(id), cancellationToken));
    }
}