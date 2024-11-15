using AppleShop.commandApplication.Commands.Category;
using FluentValidation;

namespace AppleShop.commandApplication.Validator
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                .NotEmpty().WithMessage("Id must not be empty")
                .GreaterThan(0).WithMessage("Id must be greather than 0.");

            RuleFor(x => x.Name)
           .MaximumLength(255).WithMessage("Name must be less than or equal to 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(int.MaxValue).WithMessage("Description length is too large.");

            RuleFor(x => x.IsActived)
                .GreaterThan(0).WithMessage("IsActived must be greather than 0.");
        }
    }
}