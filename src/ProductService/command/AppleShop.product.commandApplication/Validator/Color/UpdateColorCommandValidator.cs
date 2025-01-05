using AppleShop.product.commandApplication.Commands.Color;
using FluentValidation;

namespace AppleShop.product.commandApplication.Validator.Color
{
    public class UpdateColorCommandValidator : AbstractValidator<UpdateColorCommand>
    {
        public UpdateColorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.")
                .NotNull().WithMessage("Id cannot be null.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Name).MaximumLength(64).WithMessage("Name cannot exceed 64 characters.");
        }
    }
}