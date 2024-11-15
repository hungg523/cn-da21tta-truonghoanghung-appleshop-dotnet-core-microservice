﻿using AppleShop.commandApplication.Commands.Category;
using FluentValidation;

namespace AppleShop.commandApplication.Validator
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotNull().WithMessage("Name must not be null.")
            .NotEmpty().WithMessage("Name must not be empty.")
            .MaximumLength(255).WithMessage("Name must be less than or equal to 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(int.MaxValue).WithMessage("Description length is too large.");

            RuleFor(x => x.IsActived)
                .NotNull().WithMessage("IsActived must not be null.")
                .GreaterThan(0).WithMessage("IsActived must be greather than 0.");
        }
    }
}