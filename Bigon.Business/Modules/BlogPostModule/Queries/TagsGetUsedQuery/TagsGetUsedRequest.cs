using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Queries.TagsGetUsedQuery
{
    public class TagsGetUsedRequest : IRequest<IEnumerable<Tag>>
    {
    }
}
