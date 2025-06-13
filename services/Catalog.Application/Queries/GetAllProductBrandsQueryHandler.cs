using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductBrandsQuery : IRequest<IEnumerable<BrandResponse>>
{
    //
}

public class GetAllProductBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    : IRequestHandler<GetAllProductBrandsQuery, IEnumerable<BrandResponse>>
{
    public async Task<IEnumerable<BrandResponse>> Handle(GetAllProductBrandsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await brandRepository.GetProductBrands();
        return mapper.Map<IEnumerable<BrandResponse>>(result);
    }
}