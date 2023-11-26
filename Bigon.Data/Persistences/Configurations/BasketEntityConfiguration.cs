using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class BasketEntityConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.Property(m => m.UserId).HasColumnType("int").IsRequired();
            builder.Property(m => m.CatalogId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Quantity).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();

            builder.HasKey(m => new { m.UserId, m.CatalogId });
            builder.ToTable("Basket");


            builder.HasOne<ProductCatalog>()
                .WithMany()
                .HasForeignKey(p => p.CatalogId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<BigonUser>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
