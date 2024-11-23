using AppleShop.order.commandApplication.Commands.Order;
using FluentValidation;

namespace AppleShop.order.commandApplication.Validator.Order
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotNull().WithMessage("CartId must not be null.")
                .NotEmpty().WithMessage("CartId must not be empty.")
                .GreaterThan(0).WithMessage("CartId must be greather than 0.");
        }
    }
}