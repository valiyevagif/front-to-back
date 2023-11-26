using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.FaqsModule.Commands.FaqAddCommand
{
    public class FaqAddRequest : IRequest<Faq>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
