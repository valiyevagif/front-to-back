using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class SizeEntityConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(m => m.ShortName).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            builder.ConfigureAsAuditable();


            builder.HasKey(m => m.Id);
            builder.ToTable("Sizes");
        }
    }
}
