using AppleShop.promotion.Domain.Entities;
using AppleShop.promotion.queryApplication.Queries.DTOs;
using AutoMapper;

namespace AppleShop.promotion.queryApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PromotionDTO, Promotion>().ReverseMap();
        }
    }
}