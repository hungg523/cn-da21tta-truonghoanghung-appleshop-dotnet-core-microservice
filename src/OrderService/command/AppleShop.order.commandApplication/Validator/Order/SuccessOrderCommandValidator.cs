using AppleShop.order.commandApplication.Commands.Order;
using FluentValidation;

namespace AppleShop.order.commandApplication.Validator.Order
{
    public class SuccessOrderCommandValidator : AbstractValidator<SuccessOrderCommand>
    {
        public SuccessOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotNull().WithMessage("CartId must not be null.")
                .NotEmpty().WithMessage("CartId must not be empty.")
                .GreaterThan(0).WithMessage("CartId must be greather than 0.");
        }
    }
}