using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.FaqsModule.Commands.FaqEditCommand
{
    internal class FaqEditRequestHandler : IRequestHandler<FaqEditRequest, Faq>
    {
        private readonly IFaqRepository faqRepository;

        public FaqEditRequestHandler(IFaqRepository faqRepository)
        {
            this.faqRepository = faqRepository;
        }
        public async Task<Faq> Handle(FaqEditRequest request, CancellationToken cancellationToken)
        {
            //automapper
            var faq = new Faq
            {
                Id = request.Id,
                Question = request.Question,
                Answer = request.Answer,
            };

            faqRepository.Edit(faq);
            faqRepository.Save();

            return faq;
        }
    }
}
