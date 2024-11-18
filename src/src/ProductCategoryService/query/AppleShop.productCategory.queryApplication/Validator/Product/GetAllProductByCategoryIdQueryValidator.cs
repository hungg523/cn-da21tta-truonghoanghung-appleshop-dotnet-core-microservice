using AppleShop.productCategory.queryApplication.Queries.Product;
using FluentValidation;

namespace AppleShop.productCategory.queryApplication.Validator.Product
{
    public class GetAllProductByCategoryIdQueryValidator : AbstractValidator<GetAllProductByCategoryIdQuery>
    {
        public GetAllProductByCategoryIdQueryValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().WithMessage("Id must not be null.")
                     .NotEmpty().WithMessage("Id must not be empty")
                     .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}