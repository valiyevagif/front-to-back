using Bigon.Infrastructure.Entities;
using Bigon.Infrastructure.Extensions;
using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.CategoriesModule.Commands.CategoryEditCommand
{
    internal class CategoryEditRequestHandler : IRequestHandler<CategoryEditRequest, Category>
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryEditRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<Category> Handle(CategoryEditRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId
            };

            if (request.ParentId != null)
            {
                var childDetect = categoryRepository.GetAll(tracking: false).GetHierarchy(category).Any(m => m.Id == request.ParentId);

                if (childDetect)
                {
                    category.ParentId = null;
                }
            }


            categoryRepository.Edit(category);
            categoryRepository.Save();
            return category;
        }
    }
}
