using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Commands.BasketChangeQuantityCommand
{
    class BasketChangeQuantityRequestHandler : IRequestHandler<BasketChangeQuantityRequest, BasketSummary>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public BasketChangeQuantityRequestHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<BasketSummary> Handle(BasketChangeQuantityRequest request, CancellationToken cancellationToken)
        {
            var basket = new Basket
            {
                UserId = identityService.GetPrincipalId().Value,
                CatalogId = request.CatalogId,
                Quantity = request.Quantity
            };

            await productRepository.ChangeBasketQuantityAsync(basket, cancellationToken);



            var summary = await (from b in productRepository.GetBaseket(identityService.GetPrincipalId().Value)
                                 join pc in productRepository.GetCatalog() on b.CatalogId equals pc.Id
                                 join p in productRepository.GetAll() on pc.ProductId equals p.Id
                                 select new
                                 {
                                     b.Quantity,
                                     Price = pc.Price == null ? p.Price : pc.Price.Value
                                 })
                         .GroupBy(m => 1)
                         .Select(m => new BasketSummary
                         {
                             Count = m.Count(),
                             Total = m.Sum(x => x.Quantity * x.Price)
                         })
                         .FirstOrDefaultAsync(cancellationToken);

            return summary ?? new();
        }
    }
}
