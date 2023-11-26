namespace Bigon.Infrastructure.Entities
{
    public class Basket
    {
        public int UserId { get; set; }
        public int CatalogId { get; set; }
        public decimal Quantity { get; set; }
    }
}
