using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Commons.Concrates;
using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery
{
    public class BlogPostGetAllRequest : Pageable,IRequest<IPagedResponse<BlogPostGetAllDto>>
    {
        public bool OnlyPublished { get; set; }

        public override int Size
        {
            get
            {
                return base.Size < 12 ? 12 : base.Size;
            }
            set
            {
                base.Size = value < 12 ? 12 : value;
            }
        }
    }
}
