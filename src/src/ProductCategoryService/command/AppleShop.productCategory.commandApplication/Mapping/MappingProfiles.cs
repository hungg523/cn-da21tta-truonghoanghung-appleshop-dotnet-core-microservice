using AppleShop.productCategory.commandApplication.Commands.Category;
using AppleShop.productCategory.commandApplication.Commands.Color;
using AppleShop.productCategory.commandApplication.Commands.Product;
using AppleShop.productCategory.commandApplication.Commands.ProductImage;
using AppleShop.productCategory.Domain.Entities;
using AppleShop.Share.Events.Inventory.Command;
using AutoMapper;

namespace AppleShop.productCategory.commandApplication.Mapping
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

            #region Product
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ConvertUsing(new NullValueIgnoringConverter<CreateProductCommand, Product>());

            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ConvertUsing(new NullValueIgnoringConverter<UpdateProductCommand, Product>());
            #endregion

            #region Product Image
            CreateMap<CreateProductImageCommand, ProductImage>().ReverseMap();
            CreateMap<UpdateProductImageCommand, ProductImage>().ConvertUsing(new NullValueIgnoringConverter<UpdateProductImageCommand, ProductImage>());
            #endregion

            #region Color
            CreateMap<CreateColorCommand, Color>().ReverseMap();
            CreateMap<CreateColorCommand, Color>().ConvertUsing(new NullValueIgnoringConverter<CreateColorCommand, Color>());

            CreateMap<UpdateColorCommand, Color>().ReverseMap();
            CreateMap<UpdateColorCommand, Color>().ConvertUsing(new NullValueIgnoringConverter<UpdateColorCommand, Color>());
            #endregion
        }
    }
}