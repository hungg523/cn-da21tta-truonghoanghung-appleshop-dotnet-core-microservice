using AppleShop.user.commandApplication.Commands.UserAddress;
using FluentValidation;

namespace AppleShop.user.commandApplication.Validator.UserAddress
{
    public class DeleteUserAddressCommandValidator : AbstractValidator<DeleteUserAddressCommand>
    {
        public DeleteUserAddressCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull().WithMessage("CustomerId cannot be null.")
                .GreaterThan(0).WithMessage("CustomerId must be greater than 0.");
        }
    }
}