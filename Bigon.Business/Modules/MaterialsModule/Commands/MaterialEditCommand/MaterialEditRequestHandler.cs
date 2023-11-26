using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.MaterialsModule.Commands.MaterialEditCommand
{
    internal class MaterialEditRequestHandler : IRequestHandler<MaterialEditRequest, Material>
    {
        private readonly IMaterialRepository materialRepository;

        public MaterialEditRequestHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        public async Task<Material> Handle(MaterialEditRequest request, CancellationToken cancellationToken)
        {
            //automapper
            var material = new Material
            {
                Id = request.Id,
                Name = request.Name,
            };

            materialRepository.Edit(material);
            materialRepository.Save();

            return material;
        }
    }
}
