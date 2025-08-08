using Discount.Application.Commands;
using Discount.Application.Protos;
using Discount.Application.Queries;
using Grpc.Core;
using MediatR;

namespace Discount.Api.Services;

public class DiscountService(IMediator mediator) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<DeleteDiscountResponse> DeleteDiscountById(DeleteDiscountRequest request, ServerCallContext context)
    {
        var command = new DeleteDiscountByProductIdCommand(request.ProductId);
        var result = await mediator.Send(command);
        return new DeleteDiscountResponse
        {
            Success = result
        };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscountByName(DeleteDiscountRequestByName request, ServerCallContext context)
    {
        var command = new DeleteDiscountByNameCommand(request.ProductName);
        var result = await mediator.Send(command);
        return new DeleteDiscountResponse
        {
            Success = result
        };
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command = new CreateDiscountCommand(request.Coupon);
        return await mediator.Send(command);
    }
    
    public override async Task<CouponModel> GetDiscountByName(GetDiscountRequest request, ServerCallContext context)
    {
        var query = new GetDiscountByNameQuery(request.ProductName);
        var result = await mediator.Send(query);
        return result;
    }
    public override async Task<CouponModel> GetDiscountById(GetDiscountByIdRequest request, ServerCallContext context)
    {
        var query = new GetDiscountByProductIdQuery(request.ProductId);
        var result = await mediator.Send(query);
        return result;
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateDiscountCommand(request.Coupon);
        var result = await mediator.Send(command);
        return result;
    }
}