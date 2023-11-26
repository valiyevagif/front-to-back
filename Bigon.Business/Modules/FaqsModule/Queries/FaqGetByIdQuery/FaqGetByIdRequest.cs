using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.FaqsModule.Queries.FaqGetByIdQuery
{
    public class FaqGetByIdRequest : IRequest<Faq>
    {
        public int Id { get; set; }
    }
}
