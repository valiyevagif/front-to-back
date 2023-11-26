using Bigon.Business.Modules.MaterialsModule.Commands.MaterialAddCommand;
using Bigon.Business.Modules.MaterialsModule.Commands.MaterialEditCommand;
using Bigon.Business.Modules.MaterialsModule.Commands.MaterialRemoveCommand;
using Bigon.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery;
using Bigon.Business.Modules.MaterialsModule.Queries.MaterialGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialsController : Controller
    {
        private readonly IMediator mediator;
        public MaterialsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("admin.materials.index")]
        public async Task<IActionResult> Index(MaterialGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.materials.details")]
        public async Task<IActionResult> Details(MaterialGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }


        [Authorize("admin.materials.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize("admin.materials.create")]
        public async Task<IActionResult> Create(MaterialAddRequest model)
        {
            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.materials.edit")]
        public async Task<IActionResult> Edit(MaterialGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        [Authorize("admin.materials.edit")]
        public async Task<IActionResult> Edit(MaterialEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [Authorize("admin.materials.delete")]
        public async Task<IActionResult> Delete(MaterialRemoveRequest request)
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
