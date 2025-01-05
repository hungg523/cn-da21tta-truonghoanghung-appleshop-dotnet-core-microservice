using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AppleShop.cart.Domain.Entities;
using AppleShop.cart.Domain.Constant;

namespace AppleShop.cart.Persistence.Configurations
{
    public class CartItemrConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(x => x.Id).HasColumnName(CartItemConstant.FIELD_ID);
            builder.Property(x => x.ProductId).HasColumnName(CartItemConstant.FIELD_PRDUCT_ID);
            builder.Property(x => x.CartId).HasColumnName(CartConstant.FIELD_ID);
            builder.Property(x => x.Quantity).HasColumnName(CartItemConstant.FIELD_QUANTITY);
            builder.Property(x => x.UnitPrice).HasColumnName(CartItemConstant.FIELD_UNIT_PRICE);
            builder.ToTable(CartItemConstant.TABLE_CART_ITEM);

            builder.HasOne(x => x.Cart).WithMany(x => x.CartItems).HasForeignKey(x => x.CartId);
        }
    }
}