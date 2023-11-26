using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductCatalogQuery
{
    class ProductCatalogRequestHandler : IRequestHandler<ProductCatalogRequest, ProductCatalogResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IColorRepository colorRepository;
        private readonly ISizeRepository sizeRepository;
        private readonly IMaterialRepository materialRepository;

        public ProductCatalogRequestHandler(IProductRepository productRepository,
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IMaterialRepository materialRepository)
        {
            this.productRepository = productRepository;
            this.colorRepository = colorRepository;
            this.sizeRepository = sizeRepository;
            this.materialRepository = materialRepository;
        }
        public async Task<ProductCatalogResponse> Handle(ProductCatalogRequest request, CancellationToken cancellationToken)
        {
            var product = productRepository.Get(m => m.Id == request.ProductId);


            var catalog = await (from pc in productRepository.GetCatalog(m => m.ProductId == request.ProductId)
                                 join c in colorRepository.GetAll() on pc.ColorId equals c.Id
                                 join s in sizeRepository.GetAll() on pc.SizeId equals s.Id
                                 join m in materialRepository.GetAll() on pc.MaterialId equals m.Id
                                 select new ProductCatalogItem
                                 {
                                     Id = pc.Id,
                                     Color = c,
                                     Size = s,
                                     Material = m,
                                     Price = pc.Price
                                 }).ToListAsync(cancellationToken);

            var images = await productRepository.GetImages(m => m.ProductId == request.ProductId)
                .OrderByDescending(m => m.IsMain)
                .Select(m => m.Name)
                .ToArrayAsync(cancellationToken);


            return new ProductCatalogResponse
            {
                Id = request.ProductId,
                Name = product.Name,
                StockKeepingUnit = product.StockKeepingUnit,
                Rate = product.Rate,
                Price = product.Price,
                Images = images,
                Catalog = catalog
            };
        }
    }
}
