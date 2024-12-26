using AppleShop.category.commandApplication.Commands.Category;
using AppleShop.category.Domain.Entities;
using AutoMapper;

namespace AppleShop.category.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Category
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>().ConvertUsing(new NullValueIgnoringConverter<CreateCategoryCommand, Category>());
            
            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>().ConvertUsing(new NullValueIgnoringConverter<UpdateCategoryCommand, Category>());
            #endregion
        }
    }
}