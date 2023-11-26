using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Bigon.Infrastructure.Repositories
{
#nullable disable
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Size>> GetSizesForFilter();
        Task<IEnumerable<Color>> GetColorsForFilter();
        Task<IEnumerable<Material>> GetMaterialsForFilter();
        Task<IEnumerable<Brand>> GetBrandsForFilter();
        IQueryable<ProductCatalog> GetCatalog(Expression<Func<ProductCatalog, bool>> expression = null);
        IQueryable<ProductImage> GetImages(Expression<Func<ProductImage, bool>> expression = null);

        Task<Basket> AddToBasketAsync(Basket basket, CancellationToken cancellationToken);
        Task<Basket> ChangeBasketQuantityAsync(Basket basket, CancellationToken cancellationToken);
        Task RemoveFromBasketAsync(Basket basket, CancellationToken cancellationToken);
        IQueryable<Basket> GetBaseket(int userId);
        Task<string> SetRateAsync(ProductRate rate, CancellationToken cancellationToken);

        Task<string> GetPriceAsync(ProductCatalog model, CancellationToken cancellationToken);

        Task<Order> CreateOrder(Order model, int userId, CancellationToken cancellationToken);
        Task<ProductImage> AddProductImageAsync(int productId, ProductImage image, CancellationToken cancellationToken);
        void RemoveProductImage(ProductImage image);
        Task<ProductCatalog> AddProductCatalogItemAsync(int productId, ProductCatalog item, CancellationToken cancellationToken);
        Task<ProductCatalog> GetProductCatalogItemByIdAsync(int catalogId, CancellationToken cancellationToken);
        void RemoveProductCatalogItem(ProductCatalog catalogItem);
    }
#nullable enable
}
