using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public ProductBrand Brands { get; set; }
    public ProductType Types { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Product>(request);
        var result = await productRepository.CreateProduct(entity);
        //TODO
        return mapper.Map<ProductResponse>(result);
    }
}