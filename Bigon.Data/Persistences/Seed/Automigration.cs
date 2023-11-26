using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bigon.Data.Persistences.Seed
{
    public static class Automigration
    {
        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();

                db.Database.Migrate();
            }

            return builder;
        }
    }
}
