using AppleShop.user.queryApplication.Queries.UserAddress;
using FluentValidation;

namespace AppleShop.user.queryApplication.Validator.UserAddress
{
    public class GetAllAddressByUserIdQueryValidator : AbstractValidator<GetAllAddressByUserIdQuery>
    {
        public GetAllAddressByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("User Id must not be null.")
                .NotEmpty().WithMessage("User Id must not be empty.")
                .GreaterThan(0).WithMessage("User Id must be greather than 0.");
        }
    }
}