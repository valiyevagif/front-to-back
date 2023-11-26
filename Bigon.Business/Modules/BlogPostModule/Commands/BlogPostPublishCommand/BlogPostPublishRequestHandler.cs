using Bigon.Infrastructure.Repositories;
using Bigon.Infrastructure.Services.Abstracts;
using MediatR;

namespace Bigon.Business.Modules.BlogPostModule.Commands.BlogPostPublishCommand
{
    internal class BlogPostPublishRequestHandler : IRequestHandler<BlogPostPublishRequest>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly IIdentityService identityService;

        public BlogPostPublishRequestHandler(IBlogPostRepository blogPostRepository,IDateTimeService dateTimeService,IIdentityService identityService)
        {
            this.blogPostRepository = blogPostRepository;
            this.dateTimeService = dateTimeService;
            this.identityService = identityService;
        }

        public async Task Handle(BlogPostPublishRequest request, CancellationToken cancellationToken)
        {
            var entity = blogPostRepository.Get(m => m.Id == request.PostId && m.DeletedAt == null);

            if (entity.PublishedAt != null)
                throw new Exception("Bu qeyd artiq paylasilib");

            entity.PublishedAt = dateTimeService.ExecutingTime;
            entity.PublishedBy = identityService.GetPrincipalId();
            blogPostRepository.Save();
        }
    }
}
