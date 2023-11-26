using Bigon.Data.Repositories;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.SizesModule.Commands.SizeAddCommand
{
    internal class SizeAddRequestHandler : IRequestHandler<SizeAddRequest, Size>
    {
        private readonly ISizeRepository sizeRepository;

        public SizeAddRequestHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async Task<Size> Handle(SizeAddRequest request, CancellationToken cancellationToken)
        {
            var size = new Size
            {
                Name = request.Name,
                ShortName = request.ShortName,
            };

            sizeRepository.Add(size);
            sizeRepository.Save();

            return size;
        }
    }
}
