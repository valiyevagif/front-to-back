namespace Bigon.Business.Modules.ShopModule.Queries.ComplexFilterQuery
{
    public class ComplexFilterResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public double Rate { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
