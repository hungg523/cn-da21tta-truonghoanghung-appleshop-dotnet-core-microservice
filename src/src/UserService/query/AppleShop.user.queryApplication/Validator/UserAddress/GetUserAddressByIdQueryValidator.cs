using AppleShop.user.queryApplication.Queries.UserAddress;
using FluentValidation;

namespace AppleShop.user.queryApplication.Validator.UserAddress
{
    public class GetUserAddressByIdQueryValidator : AbstractValidator<GetUserAddressByIdQuery>
    {
        public GetUserAddressByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                .NotEmpty().WithMessage("Id must not be empty.")
                .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}