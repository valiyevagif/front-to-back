using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery
{
    public class MaterialGetAllRequest : IRequest<IEnumerable<Material>>
    {
    }
}
