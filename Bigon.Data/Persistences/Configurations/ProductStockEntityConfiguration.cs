using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class ProductStockEntityConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);
            builder.Property(m => m.CatalogId).HasColumnType("int").IsRequired();
            builder.Property(m => m.DocumentNo).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(m => m.Quantity).HasColumnType("int").HasPrecision(18, 2);

            builder.ConfigureAsAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("ProductStock");

            builder.HasOne<ProductCatalog>()
                .WithMany()
                .HasForeignKey(m => m.CatalogId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
