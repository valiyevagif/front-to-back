using Bigon.Infrastructure.Repositories;
using MediatR;

namespace Bigon.Business.Modules.CategoriesModule.Commands.CategoryRemoveCommand
{
    internal class CategoryRemoveRequestHandler : IRequestHandler<CategoryRemoveRequest>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryRemoveRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task Handle(CategoryRemoveRequest request, CancellationToken cancellationToken)
        {
            var category = categoryRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);

            categoryRepository.Remove(category);
            categoryRepository.Save();
        }
    }
}
