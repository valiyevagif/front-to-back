using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductGetByIdQuery
{
    internal class ProductGetByIdRequestHandler : IRequestHandler<ProductGetByIdRequest, ProductGetByIdDto>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;
        private readonly IColorRepository colorRepository;
        private readonly IMaterialRepository materialRepository;
        private readonly ISizeRepository sizeRepository;

        public ProductGetByIdRequestHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository,
            IColorRepository colorRepository, IMaterialRepository materialRepository, ISizeRepository sizeRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.colorRepository = colorRepository;
            this.materialRepository = materialRepository;
            this.sizeRepository = sizeRepository;
        }
        public async Task<ProductGetByIdDto> Handle(ProductGetByIdRequest request, CancellationToken cancellationToken)
        {
            var model = await (from p in productRepository.GetAll(m => m.DeletedBy == null)
                               join c in categoryRepository.GetAll() on p.CategoryId equals c.Id
                               join b in brandRepository.GetAll() on p.BrandId equals b.Id
                               where p.Id == request.Id
                               select new ProductGetByIdDto
                               {
                                   Id = request.Id,
                                   Name = p.Name,
                                   StockKeepingUnit = p.StockKeepingUnit,
                                   BrandId = b.Id,
                                   BrandName = b.Name,
                                   Price = p.Price,
                                   CategoryId = c.Id,
                                   CategoryName = c.Name,
                                   Rate = p.Rate,
                                   ShortDescription = p.ShortDescription,
                                   Description = p.Description,
                               }).FirstOrDefaultAsync(cancellationToken);

            model.Images = await productRepository.GetImages(m => m.ProductId == request.Id && m.DeletedAt == null)
                .ToArrayAsync(cancellationToken);

            model.Catalog = await (from c in productRepository.GetCatalog(m => m.ProductId == request.Id)
                                 join co in colorRepository.GetAll() on c.ColorId equals co.Id
                                 join m in materialRepository.GetAll() on c.MaterialId equals m.Id
                                 join s in sizeRepository.GetAll() on c.SizeId equals s.Id
                                 select new ProductCatalogItemDto
                                 {
                                     Id = c.Id,
                                     Price = c.Price,
                                     MaterialId = m.Id,
                                     MaterialName = m.Name,
                                     ColorId = co.Id,
                                     ColorName = co.Name,
                                     SizeId = s.Id,
                                     SizeName = s.Name,
                                 }).ToArrayAsync(cancellationToken);

            return model;
        }
    }
}
