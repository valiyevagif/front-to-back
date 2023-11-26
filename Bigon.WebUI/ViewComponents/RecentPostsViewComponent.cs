using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostRecentsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.ViewComponents
{
    public class RecentPostsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public RecentPostsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int size = 3)
        {
            var recents = await mediator.Send(new BlogPostRecentsRequest() { Size = size });
            return View(recents);
        }
    }
}
