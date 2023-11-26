using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.ShopModule.Commands.CreateOrderCommand
{
    class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Order>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public CreateOrderRequestHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<Order> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                ShippingAddress = request.ShippingAddress,
                ShippingCity = request.ShippingCity,
                ShippingCountry = request.ShippingCountry,
                Phone = request.Phone,
                CouponCode = request.CouponCode,
                Postcode = request.Postcode,
                Email = request.Email,
            };

            return await productRepository.CreateOrder(order, identityService.GetPrincipalId().Value, cancellationToken);            
        }
    }
}
