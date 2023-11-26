using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostRecentsQuery
{
    internal class BlogPostRecentsRequestHandler : IRequestHandler<BlogPostRecentsRequest, IEnumerable<BlogPost>>
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostRecentsRequestHandler(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IEnumerable<BlogPost>> Handle(BlogPostRecentsRequest request, CancellationToken cancellationToken)
        {
            var response = await blogPostRepository.GetAll(m => m.DeletedBy == null && m.PublishedAt != null)
                .OrderByDescending(m => m.PublishedAt)
                .Take(request.Size)
                .ToListAsync(cancellationToken);

            return response;
        }
    }
}
