using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonRoleClaimEntityConfiguration : IEntityTypeConfiguration<BigonRoleClaim>
    {
        public void Configure(EntityTypeBuilder<BigonRoleClaim> builder)
        {
            builder.ToTable("RoleClaims", "Membership");
        }
    }
}
