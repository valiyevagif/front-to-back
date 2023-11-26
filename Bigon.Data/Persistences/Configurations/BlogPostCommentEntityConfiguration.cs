using Bigon.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigon.Data.Persistences.Configurations
{
    class BlogPostCommentEntityConfiguration : IEntityTypeConfiguration<BlogPostComment>
    {
        public void Configure(EntityTypeBuilder<BlogPostComment> builder)
        {
            builder.Property(m => m.Id).UseIdentityColumn(1, 1);

            builder.Property(m => m.PostId).IsRequired().HasColumnType("int");
            builder.Property(m => m.ParentId).HasColumnType("int");
            builder.Property(m => m.Comment).HasColumnType("nvarchar(max)");
            builder.ConfigureAsAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("BlogPostComments");

            builder.HasOne<BlogPost>()
                .WithMany()
                .HasForeignKey(m => m.PostId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<BlogPostComment>()
                .WithMany()
                .HasForeignKey(m => m.ParentId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
