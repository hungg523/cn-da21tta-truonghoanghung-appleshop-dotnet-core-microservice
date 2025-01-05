using AppleShop.inventory.Domain.Entities;
using AppleShop.Share.Events.Inventory.Request;
using AutoMapper;

namespace AppleShop.inventory.commandApplication.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<InventoryCreateEvent, Inventory>().ReverseMap();
            CreateMap<InventoryCreateEvent, Inventory>().ConvertUsing(new NullValueIgnoringConverter<InventoryCreateEvent, Inventory>());

            CreateMap<InventoryUpdateEvent, Inventory>().ReverseMap();
            CreateMap<InventoryUpdateEvent, Inventory>().ConvertUsing(new NullValueIgnoringConverter<InventoryUpdateEvent, Inventory>());
        }
    }
}