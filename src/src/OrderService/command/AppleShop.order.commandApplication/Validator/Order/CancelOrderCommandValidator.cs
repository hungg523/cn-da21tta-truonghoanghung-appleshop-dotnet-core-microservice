using AppleShop.order.commandApplication.Commands.Order;
using FluentValidation;

namespace AppleShop.order.commandApplication.Validator.Order
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId must not be null.")
                .NotEmpty().WithMessage("UserId must not be empty.")
                .GreaterThan(0).WithMessage("UserId must be greather than 0.");

            RuleFor(x => x.OrderId)
               .NotNull().WithMessage("OrderId must not be null.")
               .NotEmpty().WithMessage("OrderId must not be empty.")
               .GreaterThan(0).WithMessage("OrderId must be greather than 0.");
        }
    }
}