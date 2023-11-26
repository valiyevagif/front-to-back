using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetByIdQuery
{
    internal class CategoryGetByIdRequestHandler : IRequestHandler<CategoryGetByIdRequest, CategoryGetByIdDto>
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryGetByIdRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryGetByIdDto> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            //var model = categoryRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);

            var query = from current in categoryRepository.GetAll(m => m.Id == request.Id && m.DeletedBy == null)
                        join parent in categoryRepository.GetAll() on current.ParentId equals parent.Id into ljCurrent
                        from lj in ljCurrent.DefaultIfEmpty()
                        select new CategoryGetByIdDto //projection
                        {
                            Id = current.Id,
                            Name = current.Name,
                            ParentId = current.ParentId,
                            ParentName = lj.Name
                        };


            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
