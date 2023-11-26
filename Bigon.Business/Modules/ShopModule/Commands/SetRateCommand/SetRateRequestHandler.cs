using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;

namespace Bigon.Business.Modules.ShopModule.Commands.SetRateCommand
{
    class SetRateRequestHandler : IRequestHandler<SetRateRequest, string>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public SetRateRequestHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }

        public async Task<string> Handle(SetRateRequest request, CancellationToken cancellationToken)
        {
            var rate = new ProductRate
            {
                ProductId = request.ProductId,
                Rate = request.Rate,
                UserId = identityService.GetPrincipalId().Value
            };

            var response = await productRepository.SetRateAsync(rate, cancellationToken);
            productRepository.Save();
            return response;
        }
    }
}
