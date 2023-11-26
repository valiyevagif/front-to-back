using Bigon.Business.Modules.ShopModule.Queries.BasketListQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.ViewComponents
{
    public class BasketInfoViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public BasketInfoViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new BasketListRequest());
            return View(response);
        }
    }
}
