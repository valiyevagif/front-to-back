using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class BlogPostEntityConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.Title).IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            builder.Property(m => m.Body).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(m => m.ImagePath).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(m => m.Slug).IsRequired().HasColumnType("varchar").HasMaxLength(200);
            builder.Property(m => m.CategoryId).IsRequired().HasColumnType("int");
            builder.Property(m => m.PublishedAt).HasColumnType("datetime");
            builder.Property(m => m.PublishedBy).HasColumnType("int");
            builder.ConfigureAsAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("BlogPosts");

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(m => m.CategoryId)
                .HasPrincipalKey(m=>m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(m => m.Slug).IsUnique();
        }
    }
}
