using AppleShop.promotion.queryApplication.Queries.Promotion;
using FluentValidation;

namespace AppleShop.promotion.queryApplication.Validator.Promotion
{
    public class GetPromotionByIdQueryValidator : AbstractValidator<GetPromotionByIdQuery>
    {
        public GetPromotionByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                 .NotEmpty().WithMessage("Id must not be empty")
                 .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}