using AppleShop.cart.queryApplication.Queries.Cart;
using FluentValidation;

namespace AppleShop.cart.queryApplication.Validator.Cart
{
    public class GetCartByUserIdQueryValidator : AbstractValidator<GetCartByUserIdQuery>
    {
        public GetCartByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("Id must not be null.")
                 .NotEmpty().WithMessage("Id must not be empty")
                 .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}