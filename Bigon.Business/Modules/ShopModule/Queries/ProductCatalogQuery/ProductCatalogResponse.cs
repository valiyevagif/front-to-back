using Bigon.Infrastructure.Entities;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductCatalogQuery
{
    public class ProductCatalogResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public IEnumerable<ProductCatalogItem> Catalog { get; set; }
        public string[] Images { get; set; }
        public double Rate { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductCatalogItem
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public Material Material { get; set; }
    }
}
