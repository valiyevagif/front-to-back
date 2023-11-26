using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostAddCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostEditCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostPublishCommand;
using Bigon.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand;
using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery;
using Bigon.Business.Modules.BlogPostModule.Queries.TagsGetUsedQuery;
using Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly IMediator mediator;

        public BlogPostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("admin.blogs.index")]
        public async Task<IActionResult> Index(BlogPostGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.blogs.create")]
        public async Task<IActionResult> Create()
        {
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            var tags = await mediator.Send(new TagsGetUsedRequest());
            ViewBag.Tags = new SelectList(tags, "Text", "Text");

            return View();
        }

        [HttpPost]

        [Authorize("admin.blogs.create")]
        public async Task<IActionResult> Create(BlogPostAddRequest request)
        {
            var response = await mediator.Send(request);

            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.blogs.details")]
        public async Task<IActionResult> Details(BlogPostGetByIdRequest request)
        {
            var response = await mediator.Send(request);

            return View(response);
        }

        [Authorize("admin.blogs.edit")]
        public async Task<IActionResult> Edit(BlogPostGetByIdRequest request)
        {
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            var tags = await mediator.Send(new TagsGetUsedRequest());
            ViewBag.Tags = new SelectList(tags, "Text", "Text");

            var response = await mediator.Send(request);

            return View(response);
        }

        [HttpPost]
        [Authorize("admin.blogs.edit")]
        public async Task<IActionResult> Edit(BlogPostEditRequest request)
        {
            var response = await mediator.Send(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize("admin.blogs.publish")]
        public async Task<IActionResult> Publish(BlogPostPublishRequest request)
        {
            await mediator.Send(request);

            return Json(new
            {
                error = false,
                message = "Paylaşım təsdiqləndi"
            });
        }

        [HttpPost]
        [Authorize("admin.blogs.delete")]
        public async Task<IActionResult> Delete(BlogPostRemoveRequest request)
        {
            await mediator.Send(request);

            return Json(new
            {
                error = false,
                message = "Qeyd silindi!"
            });
        }
    }
}
