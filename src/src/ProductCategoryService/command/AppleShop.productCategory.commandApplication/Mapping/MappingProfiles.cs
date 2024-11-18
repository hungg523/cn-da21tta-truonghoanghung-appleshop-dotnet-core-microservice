using AppleShop.productCategory.commandApplication.Commands.Category;
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
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<CreateCategoryCommand, Category>().ConvertUsing(new NullValueIgnoringConverter<CreateCategoryCommand, Category>());

            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>().ConvertUsing(new NullValueIgnoringConverter<UpdateCategoryCommand, Category>());

            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<CreateProductCommand, Product>().ConvertUsing(new NullValueIgnoringConverter<CreateProductCommand, Product>());

            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ConvertUsing(new NullValueIgnoringConverter<UpdateProductCommand, Product>());

            CreateMap<CreateProductImageCommand, ProductImage>().ReverseMap();
            CreateMap<UpdateProductImageCommand, ProductImage>().ConvertUsing(new NullValueIgnoringConverter<UpdateProductImageCommand, ProductImage>());
        }
    }
}