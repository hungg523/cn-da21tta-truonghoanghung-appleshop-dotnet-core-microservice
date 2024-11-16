using AppleShop.commandApplication.Commands.ProductImage;
using FluentValidation;

namespace AppleShop.commandApplication.Validator.ProductImage
{
    public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
    {
        public CreateProductImageCommandValidator()
        {
            RuleFor(x => x.Title).MaximumLength(128).WithMessage("Title cannot exceed 128 characters.");
            RuleFor(x => x.Position).GreaterThanOrEqualTo(0).WithMessage("Position must be at least 0.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId cannot be empty.")
               .NotNull().WithMessage("ProductId cannot be null.")
               .GreaterThan(0).WithMessage("ProductId must be greater than 0.");
        }
    }
}