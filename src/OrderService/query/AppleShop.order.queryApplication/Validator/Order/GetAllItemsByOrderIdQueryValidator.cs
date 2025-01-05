using AppleShop.order.queryApplication.Queries.Order;
using FluentValidation;

namespace AppleShop.order.queryApplication.Validator.Order
{
    public class GetAllItemsByOrderIdQueryValidator : AbstractValidator<GetAllItemsByOrderIdQuery>
    {
        public GetAllItemsByOrderIdQueryValidator()
        {
            RuleFor(x => x.OrderId).NotNull().WithMessage("OrderId must not be null.")
                 .NotEmpty().WithMessage("OrderId must not be empty")
                 .GreaterThan(0).WithMessage("OrderId must be greather than 0.");
        }
    }
}