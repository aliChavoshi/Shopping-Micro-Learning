using FluentValidation;
using Ordering.Application.Features.Command.CheckoutOrderV2;

namespace Ordering.Application.Features.Validators;

public class CheckoutOrderCommandValidatorsV2 : AbstractValidator<CheckoutOrderCommandV2>
{
    public CheckoutOrderCommandValidatorsV2()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("{UserName} is required")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{UserName} must not exceed 70 characters");

        RuleFor(x => x.TotalPrice)
            .NotEmpty()
            .WithMessage("{TotalPrice} is required")
            .GreaterThanOrEqualTo(1);
    }
}