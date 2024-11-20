using AppleShop.productCategory.commandApplication.Commands.Color;
using FluentValidation;

namespace AppleShop.productCategory.commandApplication.Validator.Color
{
    public class DeleteColorCommandValidator : AbstractValidator<DeleteColorCommand>
    {
        public DeleteColorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.")
                .NotNull().WithMessage("Id cannot be null.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}