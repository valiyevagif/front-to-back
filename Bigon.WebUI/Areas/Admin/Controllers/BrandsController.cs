using Bigon.Business.Modules.BrandsModule.Commands.BrandAddCommand;
using Bigon.Business.Modules.BrandsModule.Commands.BrandEditCommand;
using Bigon.Business.Modules.BrandsModule.Commands.BrandRemoveCommand;
using Bigon.Business.Modules.BrandsModule.Queries.BrandGetAllQuery;
using Bigon.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly IMediator mediator;
        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize("admin.brands.index")]
        public async Task<IActionResult> Index(BrandGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.brands.details")]
        public async Task<IActionResult> Details(BrandGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }


        [Authorize("admin.brands.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize("admin.brands.create")]
        public async Task<IActionResult> Create(BrandAddRequest model)
        {
            await mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [Authorize("admin.brands.edit")]
        public async Task<IActionResult> Edit(BrandGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        [Authorize("admin.brands.edit")]
        public async Task<IActionResult> Edit(BrandEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [Authorize("admin.brands.delete")]
        public async Task<IActionResult> Delete(BrandRemoveRequest request)
        {
            await mediator.Send(request);
            //var model = db.Brands.FirstOrDefault(m => m.Id == id && m.DeletedBy == null);

            //if (model == null)
            //{
            //    Response.Headers.Add("error", "true");

            //    string text = HttpUtility.UrlEncode("Qeyd mövcud deyil!");

            //    Response.Headers.Add("message", text);
            //    return Content("");
            //}

            //db.Brands.Remove(model);

            //db.SaveChanges();

            //var brands = db.Brands
            //    .Where(m => m.DeletedBy == null)
            //    .ToList();

            var response = await mediator.Send(new BrandGetAllRequest());

            //return View(brands);
            return PartialView("_Body", response);
        }
    }
}
