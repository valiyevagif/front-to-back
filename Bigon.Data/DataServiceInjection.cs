using Bigon.Data.Persistences;
using Bigon.Data.Persistences.Seed;
using Bigon.Infrastructure.Commons.Abstracts;
using Bigon.Infrastructure.Entities.Membership;
using Bigon.Infrastructure.Services.Concrates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bigon.Data
{
    public static class DataServiceInjection
    {
        public static IServiceCollection InstallDataServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DbContext, DataContext>(cfg =>
             {

                 cfg.UseSqlServer(configuration.GetConnectionString("cString"),
                     opt =>
                     {
                         opt.MigrationsHistoryTable("Migrations");
                     });

             });

            //services.AddIdentityCore<BigonUser>()
            //    .AddRoles<BigonRole>()
            //    .AddEntityFrameworkStores<DataContext>();
            ////.AddDefaultTokenProviders();
            

            services.AddIdentity<BigonUser, BigonRole>()
                .AddUserManager<AppUserManager>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUserManager, AppUserManager>();
            services.AddScoped<UserManager<BigonUser>>();
            services.AddScoped<RoleManager<BigonRole>>();
            services.AddScoped<SignInManager<BigonUser>>();


            var repoInterfaceType = typeof(IRepository<>);

            var concretRepositoryAssembly = typeof(DataServiceInjection).Assembly;

            var repositoryPairs = repoInterfaceType.Assembly
                                     .GetTypes()
                                     .Where(m => m.IsInterface
                                             && m.GetInterfaces()
                                                 .Any(i => i.IsGenericType
                                                     && i.GetGenericTypeDefinition() == repoInterfaceType))
                                     .Select(m => new
                                     {
                                         AbstactRepository = m,
                                         ConcrateRepository = concretRepositoryAssembly.GetTypes()
                                                              .FirstOrDefault(r => r.IsClass && m.IsAssignableFrom(r)),
                                     })
                                     .Where(m => m.ConcrateRepository != null);

            foreach (var item in repositoryPairs)
            {
                services.AddScoped(item.AbstactRepository, item.ConcrateRepository!);
            }
            return services;
        }
    }
}
