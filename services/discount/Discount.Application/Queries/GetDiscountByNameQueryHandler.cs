using AutoMapper;
using Discount.Application.Protos;
using Discount.Core.Interfaces;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Queries;

public class GetDiscountByNameQuery : IRequest<CouponModel>
{
    public string ProductName { get; set; }

    public GetDiscountByNameQuery(string productName)
    {
        ProductName = productName;
    }
}

public class GetDiscountByNameQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    : IRequestHandler<GetDiscountByNameQuery, CouponModel>
{
    public async Task<CouponModel> Handle(GetDiscountByNameQuery request, CancellationToken cancellationToken)
    {
        var entity = await discountRepository.GetDiscountByName(request.ProductName);
        if (entity == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for {request.ProductName}"));
        return mapper.Map<CouponModel>(entity);
    }
}