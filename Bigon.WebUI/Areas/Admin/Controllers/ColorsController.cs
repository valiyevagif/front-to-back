using Bigon.Business.Modules.ColorsModule.Commands.ColorAddCommand;
using Bigon.Business.Modules.ColorsModule.Commands.ColorEditCommand;
using Bigon.Business.Modules.ColorsModule.Commands.ColorRemoveCommand;
using Bigon.Business.Modules.ColorsModule.Queries.ColorGetAllQuery;
using Bigon.Business.Modules.ColorsModule.Queries.ColorGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly IMediator mediator;
        public ColorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("admin.colors.index")]
        public async Task<IActionResult> Index(ColorGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.colors.details")]
        public async Task<IActionResult> Details(ColorGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.colors.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize("admin.colors.create")]
        public async Task<IActionResult> Create(ColorAddRequest request)
        {
            await mediator.Send(request);

            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.colors.edit")]
        public async Task<IActionResult> Edit(ColorGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        [Authorize("admin.colors.edit")]
        public async Task<IActionResult> Edit(ColorEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [Authorize("admin.colors.delete")]
        public async Task<IActionResult> Delete(ColorRemoveRequest request)
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
