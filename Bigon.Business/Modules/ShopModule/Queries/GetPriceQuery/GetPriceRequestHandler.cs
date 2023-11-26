using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Queries.GetPriceQuery
{
    class GetPriceRequestHandler : IRequestHandler<GetPriceRequest, string>
    {
        private readonly IProductRepository productRepository;

        public GetPriceRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<string> Handle(GetPriceRequest request, CancellationToken cancellationToken)
        {
            var model = new ProductCatalog
            {
                ProductId = request.ProductId,
                SizeId = request.SizeId,
                ColorId = request.ColorId,
                MaterialId = request.MaterialId,
            };
            return await productRepository.GetPriceAsync(model, cancellationToken);
        }
    }
}
