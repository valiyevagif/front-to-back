using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.FaqsModule.Commands.FaqEditCommand
{
    public class FaqEditRequest : IRequest<Faq>
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
