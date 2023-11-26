using Bigon.Business;
using Bigon.Data;
using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Localize.General;
using Bigon.Infrastructure.Middlewares;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Concrates;
using Bigon.Infrastructure.Services.Configurations;
using Bigon.Infrastructure.Swagger.Filters;
using Bigon.WebApi.Pipeline;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace Bigon.WebApi
{
    public class Program
    {
        internal static string[] policies = null;
        internal static JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = true
                },
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public static void Main(string[] args)
        {
            ReadAllPolicies();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers(cfg =>
            {

                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddNewtonsoftJson(cfg =>
                {
                    cfg.SerializerSettings.NullValueHandling = JSON_SETTINGS.NullValueHandling;
                    cfg.SerializerSettings.ContractResolver = JSON_SETTINGS.ContractResolver;
                });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddCors(cfg =>
            {

                cfg.AddPolicy("allow-js", p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.WithOrigins("http://127.0.0.1:5500");
                });

            });

            DataServiceInjection.InstallDataServices(builder.Services, builder.Configuration);

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.Configure<EmailOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<CryptoOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<JwtOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddSingleton<ICryptoService, CryptoService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IBusinessReferance>());

            builder.Services.AddScoped<IValidatorInterceptor, ValidatorInterceptor>();
            builder.Services.AddValidatorsFromAssemblyContaining<IBusinessReferance>(includeInternalTypes: true);
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetParameters(builder.Configuration);


                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed,
                    OnTokenValidated = TokenValidated,
                    OnForbidden = Forbidden
                };
            });

            builder.Services.AddAuthorization(cfg =>
            {
                foreach (var policyName in policies)
                {
                    cfg.AddPolicy(policyName, p => p.RequireAssertion(handler =>
                    {
                        return handler.User.IsInRole("superadmin") || handler.User.HasClaim(policyName, "1");
                    }));
                }
            });
            builder.Services.AddSwaggerGen(cfg =>
            {
                cfg.OperationFilter<LanguageHeaderOperationFilter>();
                cfg.OperationFilter<AuthorizeOperationFilter>();
                cfg.OperationFilter<RefreshTokenHeaderOperationFilter>();

                cfg.EnableAnnotations();

                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bigon E-commerce Api",
                    Version = "v1",
                    TermsOfService = new Uri("https://github.com/kamranAeff"),
                    Contact = new OpenApiContact
                    {
                        Email = "kamran_aeff@mail.ru",
                        Name = "Kamran A-eff",
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                            ["x-instagram"] = new OpenApiObject
                            {
                                ["url"] = new OpenApiString("https://www.instagram.com/kamran_aeff"),
                                ["name"] = new OpenApiString("Instagram")
                            },
                            ["x-linkedin"] = new OpenApiObject
                            {
                                ["url"] = new OpenApiString("https://www.linkedin.com/in/kamran-aeff"),
                                ["name"] = new OpenApiString("LinkedIn")
                            }
                        }
                    }
                });
                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Tokeni bura yazin",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                             }
                         },
                         new string[]{}
                     }
                 });
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(cfg =>
                {
                    cfg.InjectStylesheet("/files/swagger-ui/custom.css");
                    cfg.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js");
                    cfg.InjectJavascript("/files/swagger-ui/custom.js");
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedCultures("en", "az", "ru");
                cfg.AddSupportedUICultures("en", "az", "ru");
                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new AppCultureProvider());
            });

            app.UseGlobalErrorHandler();
            app.UseCors("allow-js");
            app.BuildServices();

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/files",
                FileProvider = new PhysicalFileProvider(builder.Configuration.GetValue<string>("physicalPath"))
            });

            app.MapControllers();

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

        public static TokenValidationParameters GetParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtOptions:issuer"],
                ValidAudience = configuration["JwtOptions:audience"],
                ClockSkew = TimeSpan.Zero,
                //LifetimeValidator = (notBefore, expires, tokenToValidate, @param) =>
                //{
                //    return expires != null && expires > DateTime.UtcNow;
                //},
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:key"]))
            };
        }



        private static Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return Task.CompletedTask;
        }

        private static Task TokenValidated(TokenValidatedContext context)
        {
            var hasAnonymousAccess = context.HttpContext.Features.Get<IEndpointFeature>()
                ?.Endpoint
                ?.Metadata.Any(m => typeof(AllowAnonymousAttribute).IsInstanceOfType(m));

            if (hasAnonymousAccess != true && context.SecurityToken.ValidTo < DateTime.UtcNow)
            {
                context.Response.OnStarting(async () =>
                {
                    var jsonText = JsonConvert.SerializeObject(new
                    {
                        error = true,
                        message = "token_expired"
                    });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentLength = jsonText.Length;
                    await context.Response.WriteAsync(jsonText);
                });
            }
            return Task.CompletedTask;
        }

        private static Task Forbidden(ForbiddenContext context)
        {
            context.Response.OnStarting(async () =>
            {
                var jsonText = JsonConvert.SerializeObject(new
                {
                    error = true,
                    message = "insufficient_permission"
                });
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentLength = jsonText.Length;
                await context.Response.WriteAsync(jsonText);
            });
            return Task.CompletedTask;
        }
    }
}