using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Commands.BlogPostPublishCommand
{
    public class BlogPostPublishRequest : IRequest
    {
        public int PostId { get; set; }
    }
}
