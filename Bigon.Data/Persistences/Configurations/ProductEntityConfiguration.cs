using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            builder.Property(m => m.StockKeepingUnit).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            builder.Property(m => m.Rate).IsRequired().HasColumnType("decimal").HasPrecision(18, 2);
            builder.Property(m => m.Price).IsRequired().HasColumnType("decimal").HasPrecision(18, 2);
            builder.Property(m => m.ShortDescription).IsRequired().HasColumnType("nvarchar").HasMaxLength(300);
            builder.Property(m => m.Description).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(m => m.BrandId).IsRequired().HasColumnType("int");
            builder.Property(m => m.CategoryId).IsRequired().HasColumnType("int");
            //builder.ConfigureAsAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Products");
        }
    }
}
