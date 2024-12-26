using AppleShop.auth.commandApplication.Commands.Auth;
using AppleShop.auth.Domain.Entities;
using AutoMapper;

namespace AppleShop.auth.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Auth
            CreateMap<LogoutCommand, Auth>().ReverseMap();
            CreateMap<LogoutCommand, Auth>().ConvertUsing(new NullValueIgnoringConverter<LogoutCommand, Auth>());
            #endregion
        }
    }
}