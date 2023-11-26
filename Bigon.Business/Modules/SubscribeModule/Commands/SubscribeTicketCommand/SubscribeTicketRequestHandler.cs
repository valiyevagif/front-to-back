using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Bigon.Business.Modules.SubscribeModule.Commands.SubscribeTicketCommand
{
    internal class SubscribeTicketRequestHandler : IRequestHandler<SubscribeTicketRequest>
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly ICryptoService cryptoService;
        private readonly IEmailService emailService;
        private readonly IActionContextAccessor ctx;

        public SubscribeTicketRequestHandler(ISubscriberRepository subscriberRepository, 
            IDateTimeService dateTimeService, 
            ICryptoService cryptoService, 
            IEmailService emailService, 
            IActionContextAccessor ctx)
        {
            this.subscriberRepository = subscriberRepository;
            this.dateTimeService = dateTimeService;
            this.cryptoService = cryptoService;
            this.emailService = emailService;
            this.ctx = ctx;
        }

        public async Task Handle(SubscribeTicketRequest request, CancellationToken cancellationToken)
        {
            if (!request.Email.IsEmail())
                throw new Exception($"'{request.Email}' email teleblerini odemir!");

            var subscriber = subscriberRepository.Get(m => m.Email.Equals(request.Email));


            if (subscriber != null && subscriber.Approved)
                throw new Exception($"'{request.Email}' bu e-poçt adresinə artıq abunəlik tətbiq edilib!");

            else if (subscriber != null && !subscriber.Approved)
                throw new Exception($"'{request.Email}' bu e-poçt adresinin təsdiqlənməsiniz!");

            subscriber = new Subscriber();
            subscriber.Email = request.Email;
            subscriber.CreatedAt = dateTimeService.ExecutingTime;

            subscriberRepository.Add(subscriber);
            subscriberRepository.Save();

            string token = cryptoService.Encrypt($"{subscriber.Email}-{subscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon", true);

            string url = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/subscribe-approve.html?token={token}";

            string message = $"Abunəliyinizi təsdiq etmək üçün <a href=\"{url}\">linklə</a> davam edin!";
            await emailService.SendMailAsync(subscriber.Email, "Bigon Service", message);
        }
    }
}
