using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonUserRoleEntityConfiguration : IEntityTypeConfiguration<BigonUserRole>
    {
        public void Configure(EntityTypeBuilder<BigonUserRole> builder)
        {
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
