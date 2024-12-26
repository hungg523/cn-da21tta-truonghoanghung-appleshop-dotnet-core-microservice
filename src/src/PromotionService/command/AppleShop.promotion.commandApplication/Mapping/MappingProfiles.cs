using AppleShop.promotion.commandApplication.Commands.Promotion;
using AppleShop.promotion.Domain.Entities;
using AutoMapper;

namespace AppleShop.promotion.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Order
            CreateMap<CreatePromotionCommand, Promotion>().ReverseMap();
            CreateMap<CreatePromotionCommand, Promotion>().ConvertUsing(new NullValueIgnoringConverter<CreatePromotionCommand, Promotion>());

            CreateMap<UpdatePromotionCommand, Promotion>().ReverseMap();
            CreateMap<UpdatePromotionCommand, Promotion>().ConvertUsing(new NullValueIgnoringConverter<UpdatePromotionCommand, Promotion>());
            #endregion
        }
    }
}