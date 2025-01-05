using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AppleShop.order.Domain.Entities;
using AppleShop.order.Domain.Constant;

namespace AppleShop.order.Persistence.Configurations
{
    public class OrderItemrConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Id).HasColumnName(OrderItemConstant.FIELD_ID);
            builder.Property(x => x.ProductId).HasColumnName(OrderItemConstant.FIELD_PRDUCT_ID);
            builder.Property(x => x.OrderId).HasColumnName(OrderConstant.FIELD_ID);
            builder.Property(x => x.Quantity).HasColumnName(OrderItemConstant.FIELD_QUANTITY);
            builder.Property(x => x.UnitPrice).HasColumnName(OrderItemConstant.FIELD_UNIT_PRICE);
            builder.Property(x => x.TotalPrice).HasColumnName(OrderItemConstant.FIELD_TOTAL_PRICE);
            builder.ToTable(OrderItemConstant.TABLE_ORDER_ITEM);

            builder.HasOne(x => x.Order).WithMany(x => x.OrderItems).HasForeignKey(x => x.OrderId);
        }
    }
}