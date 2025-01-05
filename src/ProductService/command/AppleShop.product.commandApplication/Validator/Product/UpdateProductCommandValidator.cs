using AppleShop.product.commandApplication.Commands.Product;
using FluentValidation;

namespace AppleShop.product.commandApplication.Validator.Product
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.")
                .NotNull().WithMessage("Id cannot be null.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");
            RuleFor(x => x.Description).MaximumLength(4000).WithMessage("Description cannot exceed 4000 characters.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be at least 0.");
            RuleFor(x => x.DiscountPrice).GreaterThanOrEqualTo(0).WithMessage("Price must be at least 0.");
            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be at least 0.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
            RuleFor(x => x.IsActived).GreaterThanOrEqualTo(0).WithMessage("IsActived must be greater than or equal 0.");

            RuleFor(x => x.ColorIds).Must(list => list.All(id => id > 0)).WithMessage("ColorId must be greater than 0.");
        }
    }
}