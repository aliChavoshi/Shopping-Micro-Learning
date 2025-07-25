using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.CatalogSpecs;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetAllProductsQuery : CatalogSpecParams, IRequest<Pagination<ProductResponse>>
{
}

public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProducts(request);
        return mapper.Map<Pagination<ProductResponse>>(products);
    }
}