using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonRoleEntityConfiguration : IEntityTypeConfiguration<BigonRole>
    {
        public void Configure(EntityTypeBuilder<BigonRole> builder)
        {
            builder.ToTable("Roles","Membership");
        }
    }
}
