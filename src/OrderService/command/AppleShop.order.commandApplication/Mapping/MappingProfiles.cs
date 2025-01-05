using AppleShop.order.commandApplication.Commands.Order;
using AppleShop.order.Domain.Entities;
using AppleShop.Share.Enumerations;
using AutoMapper;

namespace AppleShop.order.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Order
            CreateMap<CancelOrderCommand, Order>().ReverseMap();
            CreateMap<CancelOrderCommand, Order>().ConvertUsing(new NullValueIgnoringConverter<CancelOrderCommand, Order>());

            CreateMap<SuccessOrderCommand, Order>().ReverseMap();
            CreateMap<SuccessOrderCommand, Order>().ConvertUsing(new NullValueIgnoringConverter<SuccessOrderCommand, Order>());
            #endregion
        }
    }
}