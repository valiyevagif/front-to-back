using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace Bigon.Infrastructure.Extensions
{
    public static partial class Extension
    {
        public static string GetHost(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
        public static string GetHeaderValue(this HttpRequest request, string key)
        {
            if (request.Headers.TryGetValue(key, out StringValues values))
                return values.First();

            return null;
        }
        public static string GetHeaderValue(this IActionContextAccessor ctx, string key)
        {
            return GetHeaderValue(ctx.ActionContext.HttpContext.Request, key);
        }

        public static IDictionary<string, object> AppendHeaderTo(this HttpRequest request, IDictionary<string, object> items, string key)
        {
            if (request.Headers.TryGetValue(key, out StringValues values))
                items.Add(key, values.First());

            return items;
        }
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }
    }
}
