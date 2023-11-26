using MediatR;
using Microsoft.EntityFrameworkCore;
using Bigon.Infrastructure.Extensions;
using System.Data;

namespace Bigon.Business.Modules.DashboardModule.Queries.CountOfStatisticQuery
{
    class CountOfStatisticRequestHandler : IRequestHandler<CountOfStatisticRequest, CountOfStatisticResponse>
    {
        private readonly DbContext db;

        public CountOfStatisticRequestHandler(DbContext db)
        {
            this.db = db;
        }

        public async Task<CountOfStatisticResponse> Handle(CountOfStatisticRequest request, CancellationToken cancellationToken)
        {
            return db.QueryFirstOrDefault<CountOfStatisticResponse>("[dbo].[spStatistics]", null, commandType: CommandType.StoredProcedure);
        }
    }
}
