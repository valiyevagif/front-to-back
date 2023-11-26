using Bigon.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.ViewComponents
{
    public class FilterBrandViewComponent : ViewComponent
    {
        private readonly IProductRepository productRepository;
        public FilterBrandViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await productRepository.GetBrandsForFilter();
            return View(items);
        }
    }
}
