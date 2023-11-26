using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.SizesModule.Queries.SizeGetByIdQuery
{
    public class SizeGetByIdRequest : IRequest<Size>
    {
        public int Id { get; set; }
    }
}
