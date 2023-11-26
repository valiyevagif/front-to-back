using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.FaqsModule.Queries.FaqGetAllQuery
{
    public class FaqGetAllRequest : IRequest<IEnumerable<Faq>>
    {
    }
}
