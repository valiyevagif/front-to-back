using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.MaterialsModule.Queries.MaterialGetByIdQuery
{
    public class MaterialGetByIdRequest : IRequest<Material>
    {
        public int Id { get; set; }
    }
}
