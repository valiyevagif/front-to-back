using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class ProductImageEntityConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(m => m.IsMain).IsRequired().HasColumnType("bit");
            builder.Property(m => m.IsMain).IsRequired().HasColumnType("bit");
            builder.Property(m => m.ProductId).IsRequired().HasColumnType("int");
            //builder.ConfigureAsAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("ProductImages");
        }
    }
}
