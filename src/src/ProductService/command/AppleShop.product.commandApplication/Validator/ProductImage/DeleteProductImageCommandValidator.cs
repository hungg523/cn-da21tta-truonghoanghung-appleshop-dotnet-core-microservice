using AppleShop.product.commandApplication.Commands.ProductImage;
using FluentValidation;

namespace AppleShop.product.commandApplication.Validator.ProductImage
{
    public class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommand>
    {
        public DeleteProductImageCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.")
                .NotNull().WithMessage("Id cannot be null.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}