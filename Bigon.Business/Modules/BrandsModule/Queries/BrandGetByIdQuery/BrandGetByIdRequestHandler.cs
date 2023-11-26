using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery
{
    internal class BrandGetByIdRequestHandler : IRequestHandler<BrandGetByIdRequest, Brand>
    {
        private readonly IBrandRepository brandRepository;
        public BrandGetByIdRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task<Brand> Handle(BrandGetByIdRequest request, CancellationToken cancellationToken)
        {
            var model = brandRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);

            return model;
        }
    }
}
