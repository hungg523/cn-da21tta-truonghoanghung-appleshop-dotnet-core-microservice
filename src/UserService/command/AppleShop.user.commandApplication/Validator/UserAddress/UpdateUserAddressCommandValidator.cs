using AppleShop.user.commandApplication.Commands.UserAddress;
using FluentValidation;

namespace AppleShop.user.commandApplication.Validator.UserAddress
{
    public class UpdateUserAddressCommandValidator : AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("CustomerId cannot be null.")
                .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");

            RuleFor(c => c.AddressLine).MaximumLength(255).WithMessage("Address Line must not exceed 255 characters.");
            RuleFor(c => c.FirstName).MaximumLength(255).WithMessage("First Name must not exceed 255 characters.");
            RuleFor(c => c.LastName).MaximumLength(255).WithMessage("Last Name must not exceed 255 characters.");
            RuleFor(c => c.PhoneNumber).MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");
            RuleFor(c => c.Province).MaximumLength(100).WithMessage("Province must not exceed 100 characters.");
            RuleFor(c => c.District).MaximumLength(100).WithMessage("District must not exceed 100 characters.");
        }
    }
}