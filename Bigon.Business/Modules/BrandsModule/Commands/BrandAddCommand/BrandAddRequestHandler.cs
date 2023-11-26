using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.BrandsModule.Commands.BrandAddCommand
{
    internal class BrandAddRequestHandler : IRequestHandler<BrandAddRequest, Brand>
    {
        private readonly IBrandRepository brandRepository;
        public BrandAddRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task<Brand> Handle(BrandAddRequest request, CancellationToken cancellationToken)
        {
            var brand = new Brand
            {
                Name = request.Name
            };

            brandRepository.Add(brand);
            brandRepository.Save();
            return brand;
        }
    }
}
