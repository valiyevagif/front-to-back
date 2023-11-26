using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class OrderDetailsEntityConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(m => m.OrderId).HasColumnType("int").IsRequired();
            builder.Property(m => m.CatalogId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Price).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
            builder.Property(m => m.Quantity).HasColumnType("int").IsRequired();

            builder.ToTable("OrderDetails");
            builder.HasKey(m => new { m.OrderId, m.CatalogId });

            builder.HasOne<Order>().WithMany().
            HasForeignKey(m => m.OrderId).
            HasPrincipalKey(m => m.Id).
            OnDelete(DeleteBehavior.NoAction);


            builder.HasOne<ProductCatalog>().WithMany().
            HasForeignKey(m => m.CatalogId).
            HasPrincipalKey(m => m.Id).
            OnDelete(DeleteBehavior.NoAction);

        }
    }
}
