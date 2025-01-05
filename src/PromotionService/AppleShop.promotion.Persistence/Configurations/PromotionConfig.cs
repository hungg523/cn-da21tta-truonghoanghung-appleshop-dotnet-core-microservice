using AppleShop.promotion.Domain.Constant;
using AppleShop.promotion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.promotion.Persistence.Configurations
{
    public class PromotionConfig : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(PromotionConstant.FIELD_ID);
            builder.Property(x => x.Code).HasColumnName(PromotionConstant.FIELD_CODE);
            builder.Property(x => x.Description).HasColumnName(PromotionConstant.FIELD_DESCRIPTION);
            builder.Property(x => x.DiscountPercentage).HasColumnName(PromotionConstant.FIELD_DISCOUNT_PERCENTAGE);
            builder.Property(x => x.TimesUsed).HasColumnName(PromotionConstant.FIELD_TIMES_USED);
            builder.Property(x => x.StartDate).HasColumnName(PromotionConstant.FIELD_START_DATE);
            builder.Property(x => x.EndDate).HasColumnName(PromotionConstant.FIELD_END_DATE);
            builder.Property(x => x.CreatedAt).HasColumnName(PromotionConstant.FIELD_CREATE_DATE);
            builder.ToTable(PromotionConstant.TABLE_PROMOTION);
        }
    }
}