using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery
{
    internal class MaterialGetAllRequestHandler : IRequestHandler<MaterialGetAllRequest, IEnumerable<Material>>
    {
        private readonly IMaterialRepository materialRepository;

        public MaterialGetAllRequestHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }

        public async Task<IEnumerable<Material>> Handle(MaterialGetAllRequest request, CancellationToken cancellationToken)
        {
            var data = materialRepository.GetAll(m => m.DeletedBy == null);

            return await data.ToListAsync(cancellationToken);
        }
    }
}
