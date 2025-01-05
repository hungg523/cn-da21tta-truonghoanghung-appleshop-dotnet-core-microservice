using AppleShop.product.commandApplication.Commands.ProductImage;
using FluentValidation;

namespace AppleShop.product.commandApplication.Validator.ProductImage
{
    public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
    {
        public UpdateProductImageCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.")
                .NotNull().WithMessage("Id cannot be null.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Title).MaximumLength(128).WithMessage("Title cannot exceed 128 characters.");
            RuleFor(x => x.Position).GreaterThanOrEqualTo(0).WithMessage("Position must be at least 0.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0.");
        }
    }
}