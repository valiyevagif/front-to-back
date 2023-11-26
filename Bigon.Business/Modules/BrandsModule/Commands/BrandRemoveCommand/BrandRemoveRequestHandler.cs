using Bigon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.BrandsModule.Commands.BrandRemoveCommand
{
    internal class BrandRemoveRequestHandler : IRequestHandler<BrandRemoveRequest>
    {
        private readonly IBrandRepository brandRepository;

        public BrandRemoveRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task Handle(BrandRemoveRequest request, CancellationToken cancellationToken)
        {
            var brand = brandRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);

            brandRepository.Remove(brand);
            brandRepository.Save();
        }
    }
}
