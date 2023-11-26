using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery
{
    internal class BlogPostGetAllRequestHandler : IRequestHandler<BlogPostGetAllRequest, IPagedResponse<BlogPostGetAllDto>>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostGetAllRequestHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task<IPagedResponse<BlogPostGetAllDto>> Handle(BlogPostGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = (from bp in blogPostRepository.GetAll(m => m.DeletedBy == null)
                         join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                         select new BlogPostGetAllDto
                         {
                             Id = bp.Id,
                             Title = bp.Title,
                             Body = bp.Body,
                             Slug = bp.Slug,
                             ImagePath = bp.ImagePath,
                             PublishedAt = bp.PublishedAt,
                             CategoryId = bp.CategoryId,
                             CategoryName = c.Name
                         });

            if (request.OnlyPublished)
            {
                return query.Where(m => m.PublishedAt!= null).ToPaging(request, m => m.PublishedAt);
            }

            return query.ToPaging(request, m => m.Id);
        }
    }
}
