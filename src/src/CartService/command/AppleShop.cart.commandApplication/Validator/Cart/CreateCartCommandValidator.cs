using AppleShop.cart.commandApplication.Commands.Category;
using AppleShop.cart.commandApplication.Commands.DTOs;
using FluentValidation;

namespace AppleShop.cart.commandApplication.Validator.Category
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId must not be null.")
            .NotEmpty().WithMessage("UserId must not be empty.")
            .GreaterThan(0).WithMessage("UserId must be greather than 0.");

            RuleForEach(x => x.CartItems).SetValidator(new CartItemDTOValidator());
        }
    }

    public class CartItemDTOValidator : AbstractValidator<CartItemDTO>
    {
        public CartItemDTOValidator()
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