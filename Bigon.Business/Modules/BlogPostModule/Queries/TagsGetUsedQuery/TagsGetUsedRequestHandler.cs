using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.BlogPostModule.Queries.TagsGetUsedQuery
{
    internal class TagsGetUsedRequestHandler : IRequestHandler<TagsGetUsedRequest,IEnumerable<Tag>>
    {
        private readonly IBlogPostRepository blogPostRepository;

        public TagsGetUsedRequestHandler(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IEnumerable<Tag>> Handle(TagsGetUsedRequest request, CancellationToken cancellationToken)
        {
            var data = await blogPostRepository.GetUsedTags().ToListAsync(cancellationToken);
            return data;
        }
    }
}
