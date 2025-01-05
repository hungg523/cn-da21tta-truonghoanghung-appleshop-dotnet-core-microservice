using AppleShop.order.commandApplication.Commands.DTOs;
using AppleShop.order.commandApplication.Commands.Order;
using FluentValidation;

namespace AppleShop.order.commandApplication.Validator.Order
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId must not be null.")
            .NotEmpty().WithMessage("UserId must not be empty.")
            .GreaterThan(0).WithMessage("UserId must be greather than 0.");

            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemDTOValidator());
        }
    }

    public class OrderItemDTOValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("ProductId must not be null.")
                .NotEmpty().WithMessage("ProductId must not be empty.")
                .GreaterThan(0).WithMessage("ProductId must be greather than 0.");

            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity must not be null.")
                .NotEmpty().WithMessage("Quantity must not be empty.")
                .GreaterThan(0).WithMessage("Quantity must be greather than 0.");
        }
    }
}