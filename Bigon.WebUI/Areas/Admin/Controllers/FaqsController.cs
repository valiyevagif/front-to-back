using Bigon.Business.Modules.FaqsModule.Commands.FaqAddCommand;
using Bigon.Business.Modules.FaqsModule.Commands.FaqEditCommand;
using Bigon.Business.Modules.FaqsModule.Commands.FaqRemoveCommand;
using Bigon.Business.Modules.FaqsModule.Queries.FaqGetAllQuery;
using Bigon.Business.Modules.FaqsModule.Queries.FaqGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqsController : Controller
    {
        private readonly IMediator mediator;
        public FaqsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("admin.faqs.index")]
        public async Task<IActionResult> Index(FaqGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.faqs.details")]
        public async Task<IActionResult> Details(FaqGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.faqs.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize("admin.faqs.create")]
        public async Task<IActionResult> Create(FaqAddRequest request)
        {
            await mediator.Send(request);

            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.faqs.edit")]
        public async Task<IActionResult> Edit(FaqGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        [Authorize("admin.faqs.edit")]
        public async Task<IActionResult> Edit(FaqEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [Authorize("admin.faqs.delete")]
        public async Task<IActionResult> Delete(FaqRemoveRequest request)
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
