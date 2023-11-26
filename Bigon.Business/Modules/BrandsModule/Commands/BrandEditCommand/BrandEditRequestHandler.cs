using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.BrandsModule.Commands.BrandEditCommand
{
    internal class BrandEditRequestHandler : IRequestHandler<BrandEditRequest, Brand>
    {
        private readonly IBrandRepository brandRepository;
        public BrandEditRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }
        public async Task<Brand> Handle(BrandEditRequest request, CancellationToken cancellationToken)
        {
            var brand = new Brand
            {
                Id = request.Id,
                Name = request.Name,
            };
            brandRepository.Edit(brand);
            brandRepository.Save();
            return brand;
        }
    }
}
