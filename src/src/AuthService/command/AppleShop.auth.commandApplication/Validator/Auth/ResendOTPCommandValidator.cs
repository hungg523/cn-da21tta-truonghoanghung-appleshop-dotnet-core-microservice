using AppleShop.auth.commandApplication.Commands.Auth;
using FluentValidation;

namespace AppleShop.auth.commandApplication.Validator.Auth
{
    public class ResendOTPCommandValidator : AbstractValidator<ResendOTPCommand>
    {
        public ResendOTPCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");
        }
    }
}