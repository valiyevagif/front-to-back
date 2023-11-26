using Bigon.Business.Modules.ShopModule.Commands.BasketAddCommand;
using Bigon.Business.Modules.ShopModule.Commands.BasketChangeQuantityCommand;
using Bigon.Business.Modules.ShopModule.Commands.CreateOrderCommand;
using Bigon.Business.Modules.ShopModule.Commands.RemoveFromBasketCommand;
using Bigon.Business.Modules.ShopModule.Commands.SetRateCommand;
using Bigon.Business.Modules.ShopModule.Queries.BasketListQuery;
using Bigon.Business.Modules.ShopModule.Queries.ComplexFilterQuery;
using Bigon.Business.Modules.ShopModule.Queries.GetPriceQuery;
using Bigon.Business.Modules.ShopModule.Queries.ProductCatalogQuery;
using Bigon.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IMediator mediator;
        public ShopController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(ComplexFilterRequest request)
        {
            request.Size = request.Size < 16 ? 16 : request.Size;

            var response = await mediator.Send(request);

            if (Request.IsAjaxRequest())
                return PartialView("_Products", response);

            return View(response);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProductCatalog(ProductCatalogRequest request)
        {
            var response = await mediator.Send(request);

            return PartialView("_ChooseProduct", response);
        }

        [AllowAnonymous]
        [Route("/shop/details/{productId}")]
        public async Task<IActionResult> Details([FromRoute] ProductCatalogRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetPrice(GetPriceRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SetRate(SetRateRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(BasketAddRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(BasketChangeQuantityRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(RemoveFromBasketRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        public async Task<IActionResult> Basket([FromRoute] BasketListRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        public IActionResult Wishlist()
        {
            return View();
        }

        public async Task<IActionResult> Checkout(BasketListRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CreateOrderRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }


    }
}
