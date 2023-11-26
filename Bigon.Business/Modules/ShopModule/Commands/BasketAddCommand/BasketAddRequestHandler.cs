using Bigon.Business.Modules.ShopModule.Queries.BasketListQuery;
using Bigon.Data.Repositories;
using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Commands.BasketAddCommand
{
    class BasketAddRequestHandler : IRequestHandler<BasketAddRequest, BasketSummary>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public BasketAddRequestHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<BasketSummary> Handle(BasketAddRequest request, CancellationToken cancellationToken)
        {
            var catalog = await productRepository.GetCatalog(m => m.ProductId == request.ProductId
            && m.ColorId == request.ColorId
            && m.SizeId == request.SizeId
            && m.MaterialId == request.MaterialId
            ).FirstOrDefaultAsync(cancellationToken);

            var basket = new Basket
            {
                UserId = identityService.GetPrincipalId().Value,
                CatalogId = catalog.Id,
                Quantity = request.Quantity <= 0 ? 1 : request.Quantity,
            };
            await productRepository.AddToBasketAsync(basket, cancellationToken);
            productRepository.Save();

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
