using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Commands.BlogPostAddCommentCommand
{
    public class BlogPostAddCommentRequest: IRequest<BlogPostCommentDto>
    {
        public int PostId { get; set; }
        public int? ParentId { get; set; }
        public string Comment { get; set; }
    }
}
