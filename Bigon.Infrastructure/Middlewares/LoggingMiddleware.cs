using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Exceptions;
using Bigon.Infrastructure.Localize.General;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Concrates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Context;
using System.Net;
using System.Net.Mime;

namespace Bigon.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IIdentityService identityService;
        private readonly IIpService ipService;

        public LoggingMiddleware(RequestDelegate next, IIdentityService identityService, IIpService ipService)
        {
            this.next = next;
            this.identityService = identityService;
            this.ipService = ipService;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var userId = identityService.GetPrincipalId();

            if (userId.HasValue)
                LogContext.PushProperty("UserId", userId.Value);

            LogContext.PushProperty("RequestIp", ipService.GetRequestIp());

            return next(httpContext);
        }
    }

    public static class LoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingMiddleware>();
            return app;
        }
    }
}
