using Microsoft.AspNetCore.Identity;

namespace Bigon.Infrastructure.Entities.Membership
{
    public class BigonUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
