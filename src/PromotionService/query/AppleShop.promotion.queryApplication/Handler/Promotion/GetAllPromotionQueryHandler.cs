using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.promotion.queryApplication.Queries.DTOs;
using AppleShop.promotion.queryApplication.Queries.Promotion;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;

namespace AppleShop.promotion.queryApplication.Handler.Promotion
{
    public class GetAllPromotionQueryHandler : IRequestHandler<GetAllPromotionQuery, Result<List<PromotionDTO>>>
    {
        private readonly IPromotionRepository orderRepository;
        private readonly IMapper mapper;

        public GetAllPromotionQueryHandler(IPromotionRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<PromotionDTO>>> Handle(GetAllPromotionQuery request, CancellationToken cancellationToken)
        {
            var promotions = orderRepository.FindAll().ToList();
            var promotionDtos = promotions.Select(promotion => mapper.Map<PromotionDTO>(promotion)).ToList();
            return Result<List<PromotionDTO>>.Ok(promotionDtos);
        }
    }
}