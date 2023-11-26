using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonUserTokenEntityConfiguration : IEntityTypeConfiguration<BigonUserToken>
    {
        public void Configure(EntityTypeBuilder<BigonUserToken> builder)
        {
            builder.Property(m => m.Type).HasColumnType("tinyint").IsRequired();
            builder.Property(m => m.ExpireDate).HasColumnType("datetime");
            builder.Property(m => m.Value).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();

            builder.HasKey(m => new { m.UserId, m.LoginProvider, m.Type, m.Value });
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
