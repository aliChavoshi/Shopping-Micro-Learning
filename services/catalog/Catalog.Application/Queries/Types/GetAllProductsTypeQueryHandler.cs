using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Types;

public class GetAllProductsTypeQuery : IRequest<IEnumerable<TypeResponse>>
{
}

public class GetAllProductsTypeQueryHandler(ITypeRepository typeRepository, IMapper mapper)
    : IRequestHandler<GetAllProductsTypeQuery, IEnumerable<TypeResponse>>
{
    public async Task<IEnumerable<TypeResponse>> Handle(GetAllProductsTypeQuery request,
        CancellationToken cancellationToken)
    {
        var result = await typeRepository.GetProductTypes();
        return mapper.Map<IEnumerable<TypeResponse>>(result);
    }
}