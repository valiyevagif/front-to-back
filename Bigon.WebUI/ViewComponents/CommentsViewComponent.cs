using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostCommentsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public CommentsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var request = new BlogPostCommentsRequest { PostId = postId };
            var response = await mediator.Send(request);
            return View(response);
        }
    }
}
