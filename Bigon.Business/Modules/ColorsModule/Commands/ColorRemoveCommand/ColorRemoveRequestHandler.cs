using Bigon.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigon.Business.Modules.ColorsModule.Commands.ColorRemoveCommand
{
    internal class ColorRemoveRequestHandler : IRequestHandler<ColorRemoveRequest>
    {
        private readonly IColorRepository colorRepository;

        public ColorRemoveRequestHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async Task Handle(ColorRemoveRequest request, CancellationToken cancellationToken)
        {
            var color = colorRepository.Get(m=>m.Id ==  request.Id);
            colorRepository.Remove(color);
            colorRepository.Save();
        }
    }
}
