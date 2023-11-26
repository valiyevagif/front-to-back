using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.SizesModule.Queries.SizeGetAllQuery
{
    public class SizeGetAllRequest : IRequest<IEnumerable<Size>>
    {
    }
}
