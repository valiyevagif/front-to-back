using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Queries.ComplexFilterQuery
{
    class ComplexFilterRequestHandler : IRequestHandler<ComplexFilterRequest, IPagedResponse<ComplexFilterResponseDto>>
    {
        private readonly IProductRepository productRepository;

        public ComplexFilterRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public async Task<IPagedResponse<ComplexFilterResponseDto>> Handle(ComplexFilterRequest request, CancellationToken cancellationToken)
        {
            var query = productRepository.GetAll(m => m.DeletedBy == null);


            if (request.Brands?.Length > 0)
                query = query.Where(m => request.Brands.Contains(m.BrandId));

            var subQuery = productRepository.GetCatalog();


            if (request.Price?.Min > 0)
            {
                query = query.Where(m => m.Price >= request.Price.Min);

                subQuery = subQuery.Where(m => m.Price == null || m.Price >= request.Price.Min);
            }

            if (request.Price?.Max > 0)
            {
                query = query.Where(m => m.Price <= request.Price.Max);

                subQuery = subQuery.Where(m => m.Price == null || m.Price <= request.Price.Max);
            }

            if (request.Sizes?.Length > 0)
                subQuery = subQuery.Where(m => request.Sizes.Contains(m.SizeId));

            if (request.Colors?.Length > 0)
                subQuery = subQuery.Where(m => request.Colors.Contains(m.ColorId));

            if (request.Materials?.Length > 0)
                subQuery = subQuery.Where(m => request.Materials.Contains(m.MaterialId));

            var productIds = await subQuery.Select(m => m.ProductId).Distinct().ToArrayAsync(cancellationToken);

            var summaryQuery = from p in query.Where(m => productIds.Contains(m.Id))
                               join pi in productRepository.GetImages(m => m.IsMain == true) on p.Id equals pi.ProductId
                               select new ComplexFilterResponseDto
                               {
                                   Id = p.Id,
                                   StockKeepingUnit = p.StockKeepingUnit,
                                   Name = p.Name,
                                   Price = p.Price,
                                   Rate = p.Rate,
                                   ImagePath = pi.Name
                               };

            return summaryQuery.ToPaging(request, m => m.Price, false);
        }
    }
}
