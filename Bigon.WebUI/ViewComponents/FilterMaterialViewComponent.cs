using Bigon.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.ViewComponents
{
    public class FilterMaterialViewComponent : ViewComponent
    {
        private readonly IProductRepository productRepository;
        public FilterMaterialViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await productRepository.GetMaterialsForFilter();
            return View(items);
        }
    }
}
