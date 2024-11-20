using AppleShop.cart.commandApplication.Commands.Cart;
using FluentValidation;

namespace AppleShop.cart.commandApplication.Validator.Cart
{
    public class DeleteProductFromCartItemCommandValidator : AbstractValidator<DeleteProductFromCartItemCommand>
    {
        public DeleteProductFromCartItemCommandValidator()
        {
            RuleFor(x => x.CartId)
                .NotNull().WithMessage("CartId must not be null.")
                .NotEmpty().WithMessage("CartId must not be empty.")
                .GreaterThan(0).WithMessage("CartId must be greather than 0.");

            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("ProductId must not be null.")
                .NotEmpty().WithMessage("ProductId must not be empty.")
                .GreaterThan(0).WithMessage("ProductId must be greather than 0.");
        }
    }
}