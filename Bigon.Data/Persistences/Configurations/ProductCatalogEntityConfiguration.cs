using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class ProductCatalogEntityConfiguration : IEntityTypeConfiguration<ProductCatalog>
    {
        public void Configure(EntityTypeBuilder<ProductCatalog> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();
            builder.Property(m => m.SizeId).HasColumnType("int").IsRequired();
            builder.Property(m => m.ColorId).HasColumnType("int").IsRequired();
            builder.Property(m => m.MaterialId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Price).HasColumnType("int").HasPrecision(18, 2);


            builder.HasKey(m => m.Id);
            builder.HasIndex(m => new { m.ProductId,m.SizeId,m.ColorId,m.MaterialId}).IsUnique();
            builder.ToTable("ProductCatalog");


            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(m => m.ProductId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Size>()
                .WithMany()
                .HasForeignKey(m => m.SizeId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Color>()
                .WithMany()
                .HasForeignKey(m => m.ColorId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Material>()
                .WithMany()
                .HasForeignKey(m => m.MaterialId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
