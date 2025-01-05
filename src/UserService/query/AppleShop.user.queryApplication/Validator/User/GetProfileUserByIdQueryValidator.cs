using AppleShop.user.queryApplication.Queries.User;
using FluentValidation;

namespace AppleShop.user.queryApplication.Validator.User
{
    public class GetProfileUserByIdQueryValidator : AbstractValidator<GetProfileUserByIdQuery>
    {
        public GetProfileUserByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                .NotEmpty().WithMessage("Id must not be empty.")
                .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}