using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.promotion.queryApplication.Queries.DTOs;
using AppleShop.promotion.queryApplication.Queries.Promotion;
using AppleShop.promotion.queryApplication.Validator.Promotion;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.promotion.Domain.Entities;

namespace AppleShop.promotion.queryApplication.Handler.Promotion
{
    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, Result<PromotionDTO>>
    {
        private readonly IPromotionRepository orderRepository;
        private readonly IMapper mapper;

        public GetPromotionByIdQueryHandler(IPromotionRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PromotionDTO>> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetPromotionByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var promotion = await orderRepository.FindByIdAsync(request.Id);
            if (promotion is null) AppleException.ThrowNotFound(typeof(Entities.Promotion));

            var orderDto = mapper.Map<PromotionDTO>(promotion);
            return Result<PromotionDTO>.Ok(orderDto);
        }
    }
}