﻿using AppleShop.promotion.commandApplication.Commands.Promotion;
using AppleShop.promotion.commandApplication.Validator.Promotion;
using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.promotion.Domain.Entities;

namespace AppleShop.promotion.commandApplication.Handler.Promotion
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand, Result<object>>
    {
        private readonly IPromotionRepository promotionRepository;
        private readonly IMapper mapper;

        public DeletePromotionCommandHandler(IPromotionRepository promotionRepository, IMapper mapper)
        {
            this.promotionRepository = promotionRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeletePromotionCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await promotionRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var promotion = await promotionRepository.FindByIdAsync(request.Id, true);
                if (promotion is null) AppleException.ThrowNotFound(typeof(Entities.Promotion));

                promotionRepository.Delete(promotion!);
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