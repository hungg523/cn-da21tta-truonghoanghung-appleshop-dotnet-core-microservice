using AppleShop.productCategory.Domain.Constant;
using AppleShop.productCategory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.productCategory.Persistence.Configurations
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