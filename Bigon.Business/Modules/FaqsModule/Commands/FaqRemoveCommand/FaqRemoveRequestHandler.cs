using Bigon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.FaqsModule.Commands.FaqRemoveCommand
{
    internal class FaqRemoveRequestHandler : IRequestHandler<FaqRemoveRequest>
    {
        private readonly IFaqRepository faqRepository;

        public FaqRemoveRequestHandler(IFaqRepository faqRepository)
        {
            this.faqRepository = faqRepository;
        }
        public async Task Handle(FaqRemoveRequest request, CancellationToken cancellationToken)
        {
            var faq = faqRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);
            faqRepository.Remove(faq);
            faqRepository.Save();
        }
    }
}
