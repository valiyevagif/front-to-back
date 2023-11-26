using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.ColorsModule.Commands.ColorEditCommand
{
    internal class ColorEditRequestHandler : IRequestHandler<ColorEditRequest, Color>
    {
        private readonly IColorRepository colorRepository;

        public ColorEditRequestHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async Task<Color> Handle(ColorEditRequest request, CancellationToken cancellationToken)
        {
            //automapper
            var color = new Color
            {
                Id = request.Id,
                Name = request.Name,
                HexCode = request.HexCode,
            };

            colorRepository.Edit(color);
            colorRepository.Save();

            return color;
        }
    }
}
