using AppleShop.order.Domain.Constant;
using AppleShop.order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.order.Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(OrderConstant.FIELD_ID);
            builder.Property(x => x.OrderStatus).HasColumnName(OrderConstant.FIELD_STATUS);
            builder.Property(x => x.Payment).HasColumnName(OrderConstant.FIELD_PAYMENT);
            builder.Property(x => x.TotalAmount).HasColumnName(OrderConstant.FIELD_TOTAL_AMOUNT);
            builder.Property(x => x.UserId).HasColumnName(OrderConstant.FIELD_USER_ID);
            builder.Property(x => x.UserAddressId).HasColumnName(OrderConstant.FIELD_USER_ADDRESS_ID);
            builder.Property(x => x.PromotionId).HasColumnName(OrderConstant.FIELD_PROMOTION_ID);
            builder.Property(x => x.CreatedAt).HasColumnName(OrderConstant.FIELD_CREATE_DATE);
            builder.ToTable(OrderConstant.TABLE_ORDER);

            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
        }
    }
}