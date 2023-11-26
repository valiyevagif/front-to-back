using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.ColorsModule.Queries.ColorGetAllQuery
{
    public class ColorGetAllRequest : IRequest<IEnumerable<Color>>
    {
    }
}
