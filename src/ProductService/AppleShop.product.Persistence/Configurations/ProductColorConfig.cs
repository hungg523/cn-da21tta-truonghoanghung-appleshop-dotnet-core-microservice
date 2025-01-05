using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AppleShop.product.Domain.Entities;
using AppleShop.product.Domain.Constant;

namespace AppleShop.product.Persistence.Configurations
{
    public class ProductColorConfig : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.Property(x => x.ColorId).HasColumnName(ColorConstant.FIELD_ID);
            builder.Property(x => x.ProductId).HasColumnName(ProductConstant.FIELD_ID);
            builder.HasKey(x => new
            {
                x.ColorId,
                x.ProductId
            });
            builder.ToTable(ColorConstant.TABLE_PRODUCT_COLOR);
        }
    }
}