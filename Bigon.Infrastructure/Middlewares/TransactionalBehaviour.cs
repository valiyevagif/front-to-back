using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bigon.Infrastructure.Middlewares
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : Attribute { }


    public class TransactionalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly DbContext db;
        private readonly IActionContextAccessor ctx;

        public TransactionalBehaviour(DbContext db, IActionContextAccessor ctx)
        {
            this.db = db;
            this.ctx = ctx;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var endpoint = ctx.ActionContext?.HttpContext.Features.Get<IEndpointFeature>()?.Endpoint;

            var attribute = endpoint?.Metadata.GetMetadata<TransactionAttribute>();

            if (attribute == null)
                return await next();

            var transaction = db.Database.CurrentTransaction ?? await db.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var response = await next();
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
