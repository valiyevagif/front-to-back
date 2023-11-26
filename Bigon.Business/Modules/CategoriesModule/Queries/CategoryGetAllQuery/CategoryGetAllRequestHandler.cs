using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetAllQuery
{
    internal class CategoryGetAllRequestHandler : IRequestHandler<CategoryGetAllRequest, IEnumerable<Category>>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryGetAllRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> Handle(CategoryGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = categoryRepository.GetAll(m => m.DeletedBy == null);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
