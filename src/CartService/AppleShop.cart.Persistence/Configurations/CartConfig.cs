using AppleShop.cart.Domain.Constant;
using AppleShop.cart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.cart.Persistence.Configurations
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(CartConstant.FIELD_ID);
            builder.Property(x => x.UserId).HasColumnName(CartConstant.FIELD_USER_ID);
            builder.Property(x => x.CreatedAt).HasColumnName(CartConstant.FIELD_CREATE_DATE);
            builder.ToTable(CartConstant.TABLE_CART);

            builder.HasMany(x => x.CartItems).WithOne().HasForeignKey(x => x.CartId);
        }
    }
}