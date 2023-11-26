using Bigon.Business.Modules.SizesModule.Commands.SizeAddCommand;
using Bigon.Business.Modules.SizesModule.Commands.SizeEditCommand;
using Bigon.Business.Modules.SizesModule.Commands.SizeRemoveCommand;
using Bigon.Business.Modules.SizesModule.Queries.SizeGetAllQuery;
using Bigon.Business.Modules.SizesModule.Queries.SizeGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizesController : Controller
    {
        private readonly IMediator mediator;
        public SizesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("admin.sizes.index")]
        public async Task<IActionResult> Index(SizeGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.sizes.details")]
        public async Task<IActionResult> Details(SizeGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.sizes.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize("admin.sizes.create")]
        public async Task<IActionResult> Create(SizeAddRequest request)
        {
            await mediator.Send(request);

            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.sizes.edit")]
        public async Task<IActionResult> Edit(SizeGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        [Authorize("admin.sizes.edit")]
        public async Task<IActionResult> Edit(SizeEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [Authorize("admin.sizes.delete")]
        public async Task<IActionResult> Delete(SizeRemoveRequest request)
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
