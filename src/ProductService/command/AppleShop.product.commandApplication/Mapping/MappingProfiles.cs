using AppleShop.product.commandApplication.Commands.Color;
using AppleShop.product.commandApplication.Commands.Product;
using AppleShop.product.commandApplication.Commands.ProductImage;
using AppleShop.product.Domain.Entities;
using AutoMapper;

namespace AppleShop.product.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
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