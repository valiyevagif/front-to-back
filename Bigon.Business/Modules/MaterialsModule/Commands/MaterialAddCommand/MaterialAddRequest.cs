using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.MaterialsModule.Commands.MaterialAddCommand
{
    public class MaterialAddRequest : IRequest<Material>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
