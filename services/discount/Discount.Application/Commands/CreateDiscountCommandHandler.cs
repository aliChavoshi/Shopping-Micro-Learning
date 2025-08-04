using AutoMapper;
using Discount.Application.Protos;
using Discount.Core.Entities;
using Discount.Core.Interfaces;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Commands;

public class CreateDiscountCommand : IRequest<CouponModel>
{
    public CouponModel CouponModel { get; set; }

    public CreateDiscountCommand(CouponModel couponModel)
    {
        CouponModel = couponModel;
    }
}

public class CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    : IRequestHandler<CreateDiscountCommand, CouponModel>
{
    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Coupon>(request.CouponModel);
        if (await discountRepository.CreateDiscount(entity))
            return request.CouponModel;

        throw new RpcException(new Status(StatusCode.Unknown, "I didn't able to create a new entity for discount"));
    }
}