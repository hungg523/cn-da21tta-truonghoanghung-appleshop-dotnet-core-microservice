using AppleShop.promotion.commandApplication.Commands.Promotion;
using FluentValidation;

namespace AppleShop.promotion.commandApplication.Validator.Promotion
{
    public class DeletePromotionCommandValidator : AbstractValidator<DeletePromotionCommand>
    {
        public DeletePromotionCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id must not be null.")
                .NotEmpty().WithMessage("Id must not be empty.")
                .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}