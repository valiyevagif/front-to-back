using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations.Membership
{
    internal class BigonUserLoginEntityConfiguration : IEntityTypeConfiguration<BigonUserLogin>
    {
        public void Configure(EntityTypeBuilder<BigonUserLogin> builder)
        {
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
