using AppleShop.commandApplication.Commands.Category;
using AppleShop.Domain.Entities;
using AutoMapper;

namespace AppleShop.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>().ConvertUsing(new NullValueIgnoringConverter<UpdateCategoryCommand, Category>());
        }
    }
}