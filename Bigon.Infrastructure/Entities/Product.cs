using Bigon.Infrastructure.Commons.Concrates;

namespace Bigon.Infrastructure.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public double Rate { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
