using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.SizesModule.Commands.SizeRemoveCommand
{
    internal class SizeRemoveRequestHandler : IRequestHandler<SizeRemoveRequest>
    {
        private readonly ISizeRepository sizeRepository;

        public SizeRemoveRequestHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }
        public async Task Handle(SizeRemoveRequest request, CancellationToken cancellationToken)
        {
            var size = sizeRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);
            sizeRepository.Remove(size);
            sizeRepository.Save();
        }
    }
}
