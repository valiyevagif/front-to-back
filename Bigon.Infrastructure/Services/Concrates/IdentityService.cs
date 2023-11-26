using Bigon.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bigon.Infrastructure.Services.Concrates
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor ctx;

        public IdentityService(IHttpContextAccessor ctx)
        {
            this.ctx = ctx;
        }
        public int? GetPrincipalId()
        {
            var userIdStr = ctx.HttpContext.User.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

            if (string.IsNullOrWhiteSpace(userIdStr))
                return null;
            
            
            return Convert.ToInt32(userIdStr);
        }
    }
}
