using AppleShop.inventory.queryApplication.Queries.Inventory;
using FluentValidation;

namespace AppleShop.inventory.queryApplication.Validator.Inventory
{
    public class GetInventoryByIdQueryValidator : AbstractValidator<GetInventoryByIdQuery>
    {
        public GetInventoryByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                 .NotEmpty().WithMessage("Id must not be empty")
                 .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}