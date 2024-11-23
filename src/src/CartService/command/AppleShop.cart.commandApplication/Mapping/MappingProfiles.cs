using AppleShop.cart.commandApplication.Commands.Cart;
using AppleShop.cart.commandApplication.Commands.DTOs;
using AppleShop.cart.Domain.Entities;
using AutoMapper;

namespace AppleShop.cart.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Cart
            CreateMap<CreateCartCommand, Cart>().ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems)).ReverseMap();
            CreateMap<CreateCartCommand, Cart>().ConvertUsing(new NullValueIgnoringConverter<CreateCartCommand, Cart>());
            
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ConvertUsing(new NullValueIgnoringConverter<CartItem, CartItemDTO>());
            #endregion
        }
    }
}