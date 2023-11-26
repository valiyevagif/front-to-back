using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostRecentsQuery
{
    public class BlogPostRecentsRequest  : IRequest<IEnumerable<BlogPost>>
    {
        public int Size { get; set; }
    }
}
