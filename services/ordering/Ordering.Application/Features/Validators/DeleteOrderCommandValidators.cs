using FluentValidation;
using Ordering.Application.Features.Command.DeleteOrder;

namespace Ordering.Application.Features.Validators;

public class DeleteOrderCommandValidators : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidators()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("{UserName} is required")
            .GreaterThan(0)
            .WithMessage("{Id} can not be -ve");
    }
}