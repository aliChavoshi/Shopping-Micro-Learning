using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductByIdQuery : IRequest<ProductResponse>
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetProductGetIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetProductById(request.Id);
        if (result == null) throw new Exception($"not found product Id ${request.Id}");
        return mapper.Map<ProductResponse>(result);
    }
}