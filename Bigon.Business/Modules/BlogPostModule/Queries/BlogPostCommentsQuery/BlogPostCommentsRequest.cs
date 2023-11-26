using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostAddCommentCommand;
using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostCommentsQuery
{
    public class BlogPostCommentsRequest : IRequest<IEnumerable<BlogPostCommentDto>>
    {
        public int PostId { get; set; }
    }
}
