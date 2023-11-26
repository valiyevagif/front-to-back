using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetAllQuery
{
    public class CategoryGetAllRequest : IRequest<IEnumerable<Category>>
    {
    }
}
