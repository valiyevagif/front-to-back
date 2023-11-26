using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.ShopModule.Queries.BasketListQuery
{
    class BasketListRequestHandler : IRequestHandler<BasketListRequest, IEnumerable<BasketListItem>>
    {
        private readonly IProductRepository productRepository;
        private readonly ISizeRepository sizeRepository;
        private readonly IColorRepository colorRepository;
        private readonly IMaterialRepository materialRepository;
        private readonly IIdentityService identityService;

        public BasketListRequestHandler(IProductRepository productRepository,
            ISizeRepository sizeRepository,
            IColorRepository colorRepository,
            IMaterialRepository materialRepository,
            IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.sizeRepository = sizeRepository;
            this.colorRepository = colorRepository;
            this.materialRepository = materialRepository;
            this.identityService = identityService;
        }

        public async Task<IEnumerable<BasketListItem>> Handle(BasketListRequest request, CancellationToken cancellationToken)
        {

            var query = from b in productRepository.GetBaseket(identityService.GetPrincipalId().Value)
                        join pc in productRepository.GetCatalog() on b.CatalogId equals pc.Id
                        join p in productRepository.GetAll() on pc.ProductId equals p.Id
                        join s in sizeRepository.GetAll() on pc.SizeId equals s.Id
                        join c in colorRepository.GetAll() on pc.ColorId equals c.Id
                        join m in materialRepository.GetAll() on pc.MaterialId equals m.Id
                        join image in productRepository.GetImages(m => m.IsMain) on p.Id equals image.ProductId
                        select new BasketListItem
                        {
                            CatalogId = pc.Id,
                            ProductId = p.Id,
                            Name = $"{p.Name} / {m.Name} / {c.Name} / {s.ShortName}",
                            ImagePath = image.Name,
                            Quantity = b.Quantity,
                            Price = pc.Price == null ? p.Price : pc.Price.Value
                        };

            return await query.ToListAsync(cancellationToken);
        }
    }
}
