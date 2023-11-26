using Bigon.Infrastructure.Commons.Concrates;
using Bigon.Infrastructure.Exceptions;
using Bigon.Infrastructure.Localize.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Mime;

namespace Bigon.Infrastructure.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                ApiResponse response = null;
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            ProcessDictionaryKeys = true
                        }
                    },
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                switch (ex)
                {
                    case NotFoundException:
                        response = ApiResponse.Fail(GeneralResource.ResourceManager.GetString(ex.Message), HttpStatusCode.NotFound);
                        break;
                    case BadRequestException bre:
                        response = ApiResponse.Fail(bre.Errors, GeneralResource.ResourceManager.GetString(ex.Message), HttpStatusCode.BadRequest);
                        break;
                    default:
                        response = ApiResponse.Fail(ex.Message, HttpStatusCode.InternalServerError);
                        break;
                }

                httpContext.Response.StatusCode = (int)response.StatusCode;
                var json = JsonConvert.SerializeObject(response, jsonSettings);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }

    public static class GlobalErrorHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            return app;
        }
    }
}
