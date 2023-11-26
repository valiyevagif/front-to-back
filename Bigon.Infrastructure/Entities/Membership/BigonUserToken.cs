using Bigon.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;

namespace Bigon.Infrastructure.Entities.Membership
{
    public class BigonUserToken : IdentityUserToken<int>
    {
        public TokenType Type { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
