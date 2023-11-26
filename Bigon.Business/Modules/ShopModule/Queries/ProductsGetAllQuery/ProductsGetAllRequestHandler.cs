using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductsGetAllQuery
{
    class ProductsGetAllRequestHandler : IRequestHandler<ProductsGetAllRequest, IPagedResponse<ProductGetAllDto>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;

        public ProductsGetAllRequestHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
        }
        public  async Task<IPagedResponse<ProductGetAllDto>> Handle(ProductsGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = (from p in productRepository.GetAll(m => m.DeletedBy == null)
                         join c in categoryRepository.GetAll() on p.CategoryId equals c.Id
                         join b in brandRepository.GetAll() on p.BrandId equals b.Id
                         join i in productRepository.GetImages(m => m.IsMain == true) on p.Id equals i.ProductId
                         select new ProductGetAllDto
                         {
                             Id = p.Id,
                             BrandId = p.BrandId,
                             BrandName = b.Name,
                             CategoryId = p.CategoryId,
                             Price = p.Price,
                             CategoryName = c.Name,
                             Rate = p.Rate,
                             ShortDescription = p.ShortDescription,
                             Name = p.Name,
                             StockKeepingUnit = p.StockKeepingUnit,
                             ImagePath = i.Name,
                         });

            return query.ToPaging(request, m => m.Id);
        }
    }
}
