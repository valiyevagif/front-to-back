using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Commands.CreateOrderCommand
{
    public class CreateOrderRequest : IRequest<Order>
    {
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CouponCode { get; set; }
    }
}
