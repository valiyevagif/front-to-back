using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetByIdQuery
{
    public class CategoryGetByIdRequest : IRequest<CategoryGetByIdDto>
    {
        public int Id { get; set; }
    }
}
