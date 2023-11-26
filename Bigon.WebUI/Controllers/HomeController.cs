using Bigon.Business.Modules.SubscribeModule.Commands.SubscribeApproveCommand;
using Bigon.Business.Modules.SubscribeModule.Commands.SubscribeTicketCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<HomeController> logger;

        public HomeController(IMediator mediator,ILogger<HomeController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            logger.LogTrace("Trace Log Message");
            logger.LogDebug("Debug Log Message");
            logger.LogInformation("Information Log Message");
            logger.LogWarning("Warning Log Message");
            logger.LogError("Error Log Message");
            logger.LogCritical("Critical Log Message");
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeTicketRequest request)
        {
            await mediator.Send(request);

            return Json(new
            {
                error = false,
                message = $"Abunəliyinizi təsdiq etmək üçün '{request.Email}' e-poçt adresinə daxil olub sizə göndərilən linkə keçid edin!"
            });
        }


        [Route("/subscribe-approve.html")]
        public async Task<IActionResult> SubscribeApprove(SubscribeApproveRequest request)
        {
            await mediator.Send(request);

            TempData["Message"] = "Abuneliyiniz tesdiqlendi";
            return RedirectToAction(nameof(Index));
        }
    }
}
