using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace Bigon.Infrastructure.Middlewares
{
    public class AppCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string lang = "az";

            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));


            if (httpContext.Request.Headers.ContainsKey("lang") 
                && httpContext.Request.Headers.TryGetValue("lang",out StringValues values) 
                && values.Any())
            {
                if (Regex.IsMatch(values.First(),@"^(en|az|ru)$"))
                    lang = values.First();
            }

            return Task.FromResult(new ProviderCultureResult(lang));
        }
    }
}
