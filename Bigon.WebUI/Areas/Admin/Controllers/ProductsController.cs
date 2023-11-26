using Bigon.Business.Modules.BrandsModule.Queries.BrandGetAllQuery;
using Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetAllQuery;
using Bigon.Business.Modules.ColorsModule.Queries.ColorGetAllQuery;
using Bigon.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery;
using Bigon.Business.Modules.ShopModule.Commands.ProductAddCommand;
using Bigon.Business.Modules.ShopModule.Commands.ProductEditCommand;
using Bigon.Business.Modules.ShopModule.Queries.ProductGetByIdQuery;
using Bigon.Business.Modules.ShopModule.Queries.ProductsGetAllQuery;
using Bigon.Business.Modules.SizesModule.Queries.SizeGetAllQuery;
using Bigon.Infrastructure.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductsController : Controller
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ProductCatalogItem()
        {
            ViewBag.ColorId = new SelectList(await mediator.Send(new ColorGetAllRequest()), "Id", "Name");
            ViewBag.SizeId = new SelectList(await mediator.Send(new SizeGetAllRequest()), "Id", "ShortName");
            ViewBag.MaterialId = new SelectList(await mediator.Send(new MaterialGetAllRequest()), "Id", "Name");
            return PartialView("_ProductCatalogItem");
        }

        [Authorize("admin.products.create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.BrandId = new SelectList(await mediator.Send(new BrandGetAllRequest()), "Id", "Name");
            ViewBag.CategoryId = new SelectList(await mediator.Send(new CategoryGetAllRequest()), "Id", "Name");
            return View();
        }

        [Authorize("admin.products.create")]
        [HttpPost]
        [Transaction]
        public async Task<IActionResult> Create(ProductAddRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [Authorize("admin.products.index")]
        public async Task<IActionResult> Index(ProductsGetAllRequest productsGetAllRequest)
        {
            var response = await mediator.Send(productsGetAllRequest);
            return View(response);
        }

        [HttpGet]
        [Authorize("admin.products.details")]
        public async Task<IActionResult> Details([FromRoute] ProductGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.products.edit")]
        public async Task<IActionResult> Edit([FromRoute] ProductGetByIdRequest request)
        {
            var response = await mediator.Send(request);

            if (response == null)
                return NotFound();

            ViewBag.BrandId = new SelectList(await mediator.Send(new BrandGetAllRequest()), "Id", "Name");
            ViewBag.CategoryId = new SelectList(await mediator.Send(new CategoryGetAllRequest()), "Id", "Name");
            ViewBag.ColorId = new SelectList(await mediator.Send(new ColorGetAllRequest()), "Id", "Name");
            ViewBag.SizeId = new SelectList(await mediator.Send(new SizeGetAllRequest()), "Id", "ShortName");
            ViewBag.MaterialId = new SelectList(await mediator.Send(new MaterialGetAllRequest()), "Id", "Name");
            return View(response);
        }

        [HttpPost]
        [Transaction]
        [Authorize("admin.products.edit")]
        public async Task<IActionResult> Edit(ProductEditRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }
    }
}
