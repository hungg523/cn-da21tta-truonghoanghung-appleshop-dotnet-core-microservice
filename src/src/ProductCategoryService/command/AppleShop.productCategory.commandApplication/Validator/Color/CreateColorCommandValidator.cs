using AppleShop.productCategory.commandApplication.Commands.Color;
using FluentValidation;

namespace AppleShop.productCategory.commandApplication.Validator.Color
{
    public class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
    {
        public CreateColorCommandValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .NotEmpty()
                .MaximumLength(64).WithMessage("Name cannot exceed 64 characters.");
        }
    }
}