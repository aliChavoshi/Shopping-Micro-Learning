using FluentValidation;
using Ordering.Application.Features.Command.UpdateOrder;

namespace Ordering.Application.Features.Validators;

public class UpdateOrderCommandValidators : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidators()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("{UserName} is required")
            .GreaterThan(0)
            .WithMessage("{Id} can not be -ve");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("{UserName} is required")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{UserName} must not exceed 70 characters");

        RuleFor(x => x.TotalPrice)
            .NotEmpty()
            .WithMessage("{TotalPrice} is required")
            .GreaterThanOrEqualTo(1)
            .WithMessage("قیمت باید بیشتر از 0 تومان باشد");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .NotNull()
            .WithMessage("{EmailAddress} is required");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{FirstName} is required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{LastName} is required");
    }
}