using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class ProductRateEntityConfiguration : IEntityTypeConfiguration<ProductRate>
    {
        public void Configure(EntityTypeBuilder<ProductRate> builder)
        {
            builder.Property(m => m.UserId).HasColumnType("int").IsRequired();
            builder.Property(m => m.ProductId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Rate).HasColumnType("int").IsRequired();

            builder.HasKey(m => new { m.UserId, m.ProductId });
            builder.ToTable("ProductRates");


            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(p => p.ProductId)
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
