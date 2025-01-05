using AppleShop.promotion.commandApplication.Commands.Promotion;
using FluentValidation;

namespace AppleShop.promotion.commandApplication.Validator.Promotion
{
    public class CreatePromotionCommandValidator : AbstractValidator<CreatePromotionCommand>
    {
        public CreatePromotionCommandValidator()
        {
            RuleFor(x => x.Code).NotNull().WithMessage("Code must not be null.")
                .NotEmpty().WithMessage("Code must be empty.")
                .MaximumLength(10).WithMessage("Code cannot exceed 10 characters.");
            RuleFor(x => x.Description).MaximumLength(512).WithMessage("Description cannot exceed 512 characters.");
            
            RuleFor(x => x.DiscountPercentage).InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");
            
            RuleFor(x => x.TimesUsed).GreaterThanOrEqualTo(0).WithMessage("Times Used must be greather than 0.");
            
            RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Start date must be greater than or equal to the current date.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to the start date.");
        }
    }
}