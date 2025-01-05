using AppleShop.auth.Domain.Constant;
using AppleShop.auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppleShop.auth.Persistence.Configurations
{
    public class AuthConfig : IEntityTypeConfiguration<Auth>
    {
        public void Configure(EntityTypeBuilder<Auth> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(AuthConstant.FIELD_ID);
            builder.Property(x => x.RefreshToken).HasColumnName(AuthConstant.FIELD_REFRESH_TOKEN);
            builder.Property(x => x.IssuedAt).HasColumnName(AuthConstant.ISSUED_AT);
            builder.Property(x => x.IsActived).HasColumnName(AuthConstant.FIELD_ISACTIVED);
            builder.Property(x => x.Expiration).HasColumnName(AuthConstant.FIELD_EXPIRATION);
            builder.Property(x => x.UserId).HasColumnName(AuthConstant.FIELD_USER_ID);
            builder.Property(x => x.RevokedAt).HasColumnName(AuthConstant.FIELD_REVOKE_AT);
            builder.ToTable(AuthConstant.TABLE_AUTH);
        }
    }
}