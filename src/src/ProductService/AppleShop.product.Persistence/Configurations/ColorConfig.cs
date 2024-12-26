using AppleShop.product.Domain.Constant;
using AppleShop.product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.product.Persistence.Configurations
{
    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(ColorConstant.FIELD_ID);
            builder.Property(x => x.Name).HasColumnName(ColorConstant.FIELD_NAME);
            builder.ToTable(ColorConstant.TABLE_COLOR);

            builder.HasMany(x => x.ProductColors).WithOne().HasForeignKey(x => x.ColorId);
        }
    }
}