using AppleShop.user.commandApplication.Commands.User;
using AppleShop.user.commandApplication.Commands.UserAddress;
using AppleShop.user.Domain.Entities;
using AutoMapper;

namespace AppleShop.user.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User
            CreateMap<User, UpdateProfileUserCommand>().ReverseMap();
            CreateMap<User, UpdateProfileUserCommand>().ConvertUsing(new NullValueIgnoringConverter<User, UpdateProfileUserCommand>());
            #endregion

            #region User Address
            CreateMap<UserAddress, CreateUserAddressCommand>().ReverseMap();
            CreateMap<UserAddress, CreateUserAddressCommand>().ConvertUsing(new NullValueIgnoringConverter<UserAddress, CreateUserAddressCommand>());

            CreateMap<UserAddress, UpdateUserAddressCommand>().ReverseMap();
            CreateMap<UserAddress, UpdateUserAddressCommand>().ConvertUsing(new NullValueIgnoringConverter<UserAddress, UpdateUserAddressCommand>());
            #endregion
        }
    }
}