﻿using AppleShop.product.Domain.Constant;
using AppleShop.product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.product.Persistence.Configurations
{
    public class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(ProductImageConstant.FIELD_ID);
            builder.Property(x => x.Title).HasColumnName(ProductImageConstant.FIELD_TITLE);
            builder.Property(x => x.Url).HasColumnName(ProductImageConstant.FIELD_URL);
            builder.Property(x => x.Position).HasColumnName(ProductImageConstant.FIELD_POSITION);
            builder.Property(x => x.ProductId).HasColumnName(ProductImageConstant.FIELD_PRDUCT_ID);
            builder.ToTable(ProductImageConstant.TABLE_PRODUCT_IMAGE);
        }
    }
}