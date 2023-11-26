using Bigon.Infrastructure.Entities;
using MediatR;

namespace Bigon.Business.Modules.CategoriesModule.Commands.CategoryAddCommand
{
    public class CategoryAddRequest : IRequest<Category>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
