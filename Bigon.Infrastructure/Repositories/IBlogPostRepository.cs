using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Entities;

namespace Bigon.Infrastructure.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        void ResolveTags(BlogPost blogPost,string[] tags);

        IQueryable<Tag> GetTagsByBlogPostId(int id);

        IQueryable<Tag> GetUsedTags();
        BlogPostComment AddComment(int postId,int? parentId,string comment);
        int CommentsCount(int postId);
        IQueryable<BlogPostComment> GetComments(int postId);
    }
}
