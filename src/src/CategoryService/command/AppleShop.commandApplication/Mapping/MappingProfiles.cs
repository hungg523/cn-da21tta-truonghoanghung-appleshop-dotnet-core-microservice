using AppleShop.commandApplication.Commands.Category;
using AppleShop.commandApplication.Commands.Product;
using AppleShop.commandApplication.Commands.ProductImage;
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

            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ConvertUsing(new NullValueIgnoringConverter<UpdateProductCommand, Product>());

            CreateMap<CreateProductImageCommand, ProductImage>().ReverseMap();
            CreateMap<UpdateProductImageCommand, ProductImage>().ConvertUsing(new NullValueIgnoringConverter<UpdateProductImageCommand, ProductImage>());
        }
    }
}