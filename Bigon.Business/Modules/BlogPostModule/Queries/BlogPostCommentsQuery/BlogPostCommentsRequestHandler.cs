using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostAddCommentCommand;
using Bigon.Infrastructure.Entities.Membership;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostCommentsQuery
{
    internal class BlogPostCommentsRequestHandler : IRequestHandler<BlogPostCommentsRequest, IEnumerable<BlogPostCommentDto>>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly UserManager<BigonUser> userManager;

        public BlogPostCommentsRequestHandler(IBlogPostRepository blogPostRepository, UserManager<BigonUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<BlogPostCommentDto>> Handle(BlogPostCommentsRequest request, CancellationToken cancellationToken)
        {
            var response = await (from c in blogPostRepository.GetComments(request.PostId)
                                  join u in userManager.Users on c.CreatedBy equals u.Id
                                  select new BlogPostCommentDto
                                  {
                                      Id = c.Id,
                                      ParentId = c.ParentId,
                                      PostId = c.PostId,
                                      Comment = c.Comment,
                                      CreatedAt = c.CreatedAt,
                                      Author = $"{u.Name} {u.Surname}"
                                  }).ToListAsync(cancellationToken);


            return GetChildComments(response, null);
        }




        public IEnumerable<BlogPostCommentDto> GetChildComments(IEnumerable<BlogPostCommentDto> comments, BlogPostCommentDto parent = null)
        {
            if (parent == null)
            {
                foreach (var item in comments.Where(m => m.ParentId == null))
                {
                    yield return item;

                    foreach (var child in comments.Where(m => m.ParentId == item.Id).SelectMany(m => GetChildComments(comments, m)))
                    {
                        yield return child;
                    }
                }
            }
            else
            {
                if (parent.ParentId != null)
                    yield return parent;

                foreach (var item in comments.Where(m => m.ParentId == parent.Id).SelectMany(m => GetChildComments(comments, m)))
                {
                    yield return item;
                }
            }
        }
    }
}
