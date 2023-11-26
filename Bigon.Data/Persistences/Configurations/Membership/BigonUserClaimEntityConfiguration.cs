using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonUserClaimEntityConfiguration : IEntityTypeConfiguration<BigonUserClaim>
    {
        public void Configure(EntityTypeBuilder<BigonUserClaim> builder)
        {
            builder.ToTable("UserClaims", "Membership");
        }
    }
}
