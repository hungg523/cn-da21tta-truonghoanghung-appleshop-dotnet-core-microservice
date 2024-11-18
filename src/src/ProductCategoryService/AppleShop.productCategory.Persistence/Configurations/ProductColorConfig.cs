using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AppleShop.productCategory.Domain.Entities;
using AppleShop.productCategory.Domain.Constant;

namespace AppleShop.productCategory.Persistence.Configurations
{
    public class ProductColorConfig : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(x => new
            {
                x.ColorId,
                x.ProductId
            });
            builder.Property(x => x.ColorId).HasColumnName(ColorConstant.FIELD_ID);
            builder.Property(x => x.ProductId).HasColumnName(ProductConstant.FIELD_ID);
            builder.ToTable(ColorConstant.TABLE_COLOR);
        }
    }
}