using Bigon.Infrastructure.Entities.Membership;
using System.Security.Claims;

namespace Bigon.Infrastructure.Services.Abstracts
{
    public interface IJwtService
    {
        string GenerateAccessToken(BigonUser user);
    }
}
