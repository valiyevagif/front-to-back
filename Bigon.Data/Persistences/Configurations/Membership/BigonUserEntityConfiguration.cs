using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonUserEntityConfiguration : IEntityTypeConfiguration<BigonUser>
    {
        public void Configure(EntityTypeBuilder<BigonUser> builder)
        {
            builder.ToTable("Users", "Membership");
        }
    }
}
