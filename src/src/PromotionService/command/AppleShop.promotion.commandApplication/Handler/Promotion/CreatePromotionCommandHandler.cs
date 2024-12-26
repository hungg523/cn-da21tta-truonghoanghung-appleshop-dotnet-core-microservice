using AppleShop.promotion.commandApplication.Commands.Promotion;
using AppleShop.promotion.commandApplication.Validator.Promotion;
using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.promotion.Domain.Entities;

namespace AppleShop.promotion.commandApplication.Handler.Promotion
{
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, Result<object>>
    {
        private readonly IPromotionRepository promotionRepository;
        private readonly IMapper mapper;

        public CreatePromotionCommandHandler(IPromotionRepository promotionRepository, IMapper mapper)
        {
            this.promotionRepository = promotionRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePromotionCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var promotion = mapper.Map<Entities.Promotion>(request);
            using var transaction = await promotionRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                promotion.Code = request.Code.ToUpper();
                promotionRepository.Create(promotion);
                await promotionRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Result<object>.Ok();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}