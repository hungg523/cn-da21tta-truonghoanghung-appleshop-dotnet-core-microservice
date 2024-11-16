using AppleShop.Domain.Entities;
using AppleShop.queryApplication.Queries.DTOs;
using AutoMapper;

namespace AppleShop.queryApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductFullDTO, Product>().ReverseMap();
            CreateMap<ProductFullDTO, Product>().ConvertUsing(new NullValueIgnoringConverter<ProductFullDTO, Product>());

            CreateMap<ProductFullDTO, ProductImageDTO>().ReverseMap();
            CreateMap<ProductFullDTO, ProductImageDTO>().ConvertUsing(new NullValueIgnoringConverter<ProductFullDTO, ProductImageDTO>());

            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
            CreateMap<ProductImage, ProductImageDTO>().ConvertUsing(new NullValueIgnoringConverter<ProductImage, ProductImageDTO>());
        }
    }
}