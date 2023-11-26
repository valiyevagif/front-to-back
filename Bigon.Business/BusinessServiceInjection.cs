using Bigon.Data.Persistences.Seed;
using Microsoft.AspNetCore.Builder;

namespace Bigon.Business
{
    public static class BusinessServiceInjection
    {

        public static IApplicationBuilder BuildServices(this IApplicationBuilder app)
        {
            app.UpdateDatabase();
            return app;
        }
    }
}
