using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.BrandsModule.Queries.BrandGetAllQuery
{
    internal class BrandGetAllRequestHandler : IRequestHandler<BrandGetAllRequest, IEnumerable<Brand>>
    {
        private readonly IBrandRepository brandRepository;

        public BrandGetAllRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> Handle(BrandGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = brandRepository.GetAll(m => m.DeletedBy == null);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
