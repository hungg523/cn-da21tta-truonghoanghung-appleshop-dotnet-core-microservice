using AppleShop.queryApplication.Queries.Category;
using FluentValidation;

namespace AppleShop.queryApplication.Validator.Category
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                 .NotEmpty().WithMessage("Id must not be empty")
                 .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}