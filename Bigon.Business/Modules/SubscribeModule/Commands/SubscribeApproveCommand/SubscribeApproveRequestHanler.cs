using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Bigon.Business.Modules.SubscribeModule.Commands.SubscribeApproveCommand
{
    internal class SubscribeApproveRequestHanler : IRequestHandler<SubscribeApproveRequest>
    {
        private readonly ICryptoService cryptoService;
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IDateTimeService dateTimeService;

        public SubscribeApproveRequestHanler(ICryptoService cryptoService, ISubscriberRepository subscriberRepository, IDateTimeService dateTimeService)
        {
            this.cryptoService = cryptoService;
            this.subscriberRepository = subscriberRepository;
            this.dateTimeService = dateTimeService;
        }

        public async Task Handle(SubscribeApproveRequest request, CancellationToken cancellationToken)
        {
            request.Token = cryptoService.Decrypt(request.Token);

            string pattern = @"(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";

            Match match = Regex.Match(request.Token, pattern);

            if (!match.Success)
                throw new Exception("token zedelidir!");

            string email = match.Groups["email"].Value;
            string dateStr = match.Groups["date"].Value;

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out DateTime date))
                throw new Exception("token zedelidir!");

            var subscriber = subscriberRepository.Get(m => m.Email.Equals(email) && m.CreatedAt == date);

            if (subscriber == null)
                throw new Exception("token zedelidir!");

            if (!subscriber.Approved)
            {
                subscriber.Approved = true;
                subscriber.ApprovedAt = dateTimeService.ExecutingTime;
            }
            subscriberRepository.Save();
        }
    }
}
