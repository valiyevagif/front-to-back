using Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;
        public CategoriesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new CategoryGetAllRequest());
            return View(response);
        }
    }
}
