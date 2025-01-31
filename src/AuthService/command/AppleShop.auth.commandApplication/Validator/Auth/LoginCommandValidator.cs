﻿using AppleShop.auth.commandApplication.Commands.Auth;
using FluentValidation;

namespace AppleShop.auth.commandApplication.Validator.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MaximumLength(255).WithMessage("Password cannot exceed 255 characters.");
        }
    }
}