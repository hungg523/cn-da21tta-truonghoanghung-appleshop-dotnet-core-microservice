using AppleShop.inventory.Domain.Constant;
using AppleShop.inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.inventory.Persistence.Configurations
{
    public class InventoryConfig : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(InventoryConstant.FIELD_ID);
            builder.Property(x => x.Stock).HasColumnName(InventoryConstant.FIELD_STOCK);
            builder.Property(x => x.ProductId).HasColumnName(InventoryConstant.FIELD_PRODUCT_ID);
            builder.Property(x => x.LastUpdated).HasColumnName(InventoryConstant.FIELD_LAST_UPDATED);
            builder.ToTable(InventoryConstant.TABLE_INVENTORY);
        }
    }
}