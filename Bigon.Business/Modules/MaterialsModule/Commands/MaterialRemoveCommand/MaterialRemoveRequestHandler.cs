using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.MaterialsModule.Commands.MaterialRemoveCommand
{
    internal class MaterialRemoveRequestHandler : IRequestHandler<MaterialRemoveRequest>
    {
        private readonly IMaterialRepository materialRepository;

        public MaterialRemoveRequestHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        public async Task Handle(MaterialRemoveRequest request, CancellationToken cancellationToken)
        {
            var material = materialRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);
            materialRepository.Remove(material);
            materialRepository.Save();
        }
    }
}
