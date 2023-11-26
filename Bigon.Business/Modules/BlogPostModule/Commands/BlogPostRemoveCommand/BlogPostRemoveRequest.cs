using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand
{
    public class BlogPostRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
