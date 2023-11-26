using Bigon.Business.Modules.ShopModule.Queries.ProductCatalogQuery;
using Bigon.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.ShopModule.Queries.ProductGetByIdQuery
{
    public class ProductGetByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rate { get; set; }
        public string StockKeepingUnit { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ProductImage[] Images { get; set; }
        public ProductCatalogItemDto[] Catalog { get; set; }
    }


    public class ProductCatalogItemDto
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal? Price { get; set; }
    }
}
