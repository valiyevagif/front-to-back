using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Data.Repositories
{
    internal class BlogPostRepository : GeneralRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(DbContext db) : base(db)
        {
        }

        BlogPostTag AddTag(int blogPostId, string tag)
        {
            var tagsTable = db.Set<Tag>();
            var blogPostTagsTable = db.Set<BlogPostTag>();

            var tagEntity = tagsTable.FirstOrDefault(m => m.Text.Equals(tag));

            if (tagEntity == null)
            {
                tagEntity = new Tag { Text = tag };
                tagsTable.Add(tagEntity);
                base.Save();
            }

            var blogPostTag = blogPostTagsTable.Where(m => m.TagId == tagEntity.Id && m.BlogPostId == blogPostId).FirstOrDefault();

            if (blogPostTag == null)
            {
                blogPostTag = new BlogPostTag
                {
                    TagId = tagEntity.Id,
                    BlogPostId = blogPostId
                };
                blogPostTagsTable.Add(blogPostTag);
            }

            return blogPostTag;
        }

        public void ResolveTags(BlogPost blogPost, string[] tags)
        {
            if (tags == null || tags.Length == 0)
                return;


            var tagsTable = db.Set<Tag>();
            var blogPostTagsTable = db.Set<BlogPostTag>();

            var assignedTagsQuery = from bpt in blogPostTagsTable
                                    join t in tagsTable on bpt.TagId equals t.Id
                                    where bpt.BlogPostId == blogPost.Id
                                    select new
                                    {
                                        TagId = bpt.TagId,
                                        BlogPostId = bpt.BlogPostId,
                                        Text = t.Text,
                                        BlogPostTag = bpt
                                    };

            var forDeletion = assignedTagsQuery.Where(m => !tags.Contains(m.Text)).Select(m => m.BlogPostTag).ToList();

            blogPostTagsTable.RemoveRange(forDeletion);

            var forInsertion = tags.Except(assignedTagsQuery.Select(m => m.Text).ToList());

            foreach (var tag in forInsertion)
            {
                AddTag(blogPost.Id, tag);
            }
        }

        public IQueryable<Tag> GetTagsByBlogPostId(int id)
        {
            var query = from bpt in db.Set<BlogPostTag>()
                        join t in db.Set<Tag>() on bpt.TagId equals t.Id
                        where bpt.BlogPostId == id
                        select t;

            return query;
        }

        public IQueryable<Tag> GetUsedTags()
        {
            var query = (from bpt in db.Set<BlogPostTag>()
                         join t in db.Set<Tag>() on bpt.TagId equals t.Id
                         select t).Distinct();
            return query;
        }

        public BlogPostComment AddComment(int postId, int? parentId, string comment)
        {
            var commentsTable = db.Set<BlogPostComment>();

            var commentEntity = new BlogPostComment
            {
                PostId = postId,
                ParentId = parentId,
                Comment = comment
            };

            commentsTable.Add(commentEntity);
            return commentEntity;
        }

        public int CommentsCount(int postId)
        {
            return db.Set<BlogPostComment>().Count(m => m.PostId == postId && m.DeletedBy == null);
        }

        public IQueryable<BlogPostComment> GetComments(int postId)
        {
            return db.Set<BlogPostComment>().Where(m => m.PostId == postId && m.DeletedBy == null);
        }
    }
}
