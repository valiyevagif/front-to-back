using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class BlogPostTagEntityConfiguration : IEntityTypeConfiguration<BlogPostTag>
    {
        public void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.HasOne<BlogPost>()
                .WithMany()
                .HasForeignKey(m => m.BlogPostId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey(m => m.TagId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(m => new { m.TagId, m.BlogPostId });
            builder.ToTable("BlogPostTags");
        }
    }
}
