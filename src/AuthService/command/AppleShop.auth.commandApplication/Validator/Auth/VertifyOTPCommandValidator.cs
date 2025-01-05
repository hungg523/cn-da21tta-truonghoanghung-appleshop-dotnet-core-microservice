using AppleShop.auth.commandApplication.Commands.Auth;
using FluentValidation;

namespace AppleShop.auth.commandApplication.Validator.Auth
{
    public class VertifyOTPCommandValidator : AbstractValidator<VertifyOTPCommand>
    {
        public VertifyOTPCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

            RuleFor(x => x.OTP).Length(6).WithMessage("OTP must be exceed 6 characters.");
        }
    }
}