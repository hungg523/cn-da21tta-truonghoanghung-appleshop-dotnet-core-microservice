using AppleShop.auth.queryApplication.Queries.Order;
using FluentValidation;

namespace AppleShop.auth.queryApplication.Validator.Order
{
    public class GetOrderByUserIdQueryValidator : AbstractValidator<GetOrderByUserIdQuery>
    {
        public GetOrderByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("Id must not be null.")
                 .NotEmpty().WithMessage("Id must not be empty")
                 .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}