using AppleShop.queryApplication.Queries.Product;
using FluentValidation;

namespace AppleShop.queryApplication.Validator.Product
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                 .NotEmpty().WithMessage("Id must not be empty")
                 .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}