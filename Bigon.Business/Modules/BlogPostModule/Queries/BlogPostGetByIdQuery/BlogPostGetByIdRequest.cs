using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery
{
    public class BlogPostGetByIdRequest : IRequest<BlogPostGetByIdDto>
    {
        public int Id { get; set; }
    }
}
