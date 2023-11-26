using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class FaqEntityConfiguration : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.Question).IsRequired().HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(m => m.Answer).IsRequired().HasColumnType("nvarchar(max)");
            //builder.ConfigureAsAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Faqs");
        }
    }
}
