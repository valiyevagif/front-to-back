using Bigon.Business;
using Bigon.Data;
using Bigon.Infrastructure.Middlewares;
using Bigon.Infrastructure.Serilog;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Concrates;
using Bigon.Infrastructure.Services.Configurations;
using Bigon.WebUI.Pipeline;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using System.Data;
using System.Reflection;

namespace Bigon.WebUI
{
    public class Program
    {

        internal static string[] policies = null;
        public static void Main(string[] args)
        {
            ReadAllPolicies();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(cfg =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));

                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

            });

            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .WriteTo.MSSqlServer(
                            connectionString: builder.Configuration.GetConnectionString("cString"),
                            sinkOptions: new SqlServerSinkOptions(),
                            columnOptions: new SqlServerColumnOptions()
                            )
                    .CreateLogger();

            builder.Host.UseSerilog();

            DataServiceInjection.InstallDataServices(builder.Services, builder.Configuration);

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.Configure<EmailOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<CryptoOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<JwtOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddSingleton<ICryptoService, CryptoService>();
            builder.Services.AddSingleton<IIpService, IpService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehaviour<,>));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IBusinessReferance).Assembly));

            builder.Services.AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    cfg.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(cfg =>
                {
                    cfg.LoginPath = "/signin.html";
                    cfg.AccessDeniedPath = "/accessdenied.html";
                    cfg.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                    cfg.Cookie.Name = "bigon";
                    cfg.Cookie.HttpOnly = true;
                });

            builder.Services.AddAuthorization(cfg =>
            {
                foreach (var policyName in policies)
                {
                    cfg.AddPolicy(policyName, p => p.RequireAssertion(handler => handler.User.IsInRole("superadmin") || handler.User.HasClaim(policyName, "1")));
                }
            });

            builder.Services.Configure<IdentityOptions>(cfg =>
            {

                cfg.User.RequireUniqueEmail = true;
                //cfg.User.AllowedUserNameCharacters = "abcdef123456789";

                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireNonAlphanumeric = false;


                cfg.Lockout.AllowedForNewUsers = true;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                cfg.Lockout.MaxFailedAccessAttempts = 3;

            });

            var app = builder.Build();

            app.BuildServices();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSerilog();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                    );

                cfg.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");

            });

            app.Run();
        }

        private static void ReadAllPolicies()
        {
            var types = typeof(Program).Assembly.GetTypes();

            policies = types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
                .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                .Union(
                types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                 && !method.IsDefined(typeof(NonActionAttribute), true)
                 && method.IsDefined(typeof(AuthorizeAttribute), true))
                 .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                )
                .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                .SelectMany(a => a.Policy.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .ToArray();
        }
    }
}