using AppleShop.user.commandApplication.Commands.User;
using FluentValidation;

namespace AppleShop.user.commandApplication.Validator.User
{
    public class UpdateProfileUserCommandValidator : AbstractValidator<UpdateProfileUserCommand>
    {
        public UpdateProfileUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("CustomerId cannot be null.")
                .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");
            RuleFor(x => x.UserName).MaximumLength(255).WithMessage("Username must not exceed 255 characters.");
        }
    }
}