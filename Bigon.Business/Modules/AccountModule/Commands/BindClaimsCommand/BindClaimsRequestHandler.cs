using Bigon.Infrastructure.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bigon.Business.Modules.AccountModule.Commands.BindClaimsCommand
{
    internal class BindClaimsRequestHandler : IRequestHandler<BindClaimsRequest>
    {
        private readonly DbContext db;
        private readonly UserManager<BigonUser> userManager;

        public BindClaimsRequestHandler(DbContext db, UserManager<BigonUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task Handle(BindClaimsRequest request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(request.Identity.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value);

            var user = await db.Set<BigonUser>().FirstOrDefaultAsync(m => m.Id == userId, cancellationToken);

            request.Identity.AddClaim(new Claim(ClaimTypes.GivenName, $"{user.Name} {user.Surname}"));


            var roles = await userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                request.Identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }

            var claims = await (from uc in db.Set<BigonUserClaim>()
                                where uc.UserId == userId && uc.ClaimValue.Equals("1")
                                select uc.ClaimType)
                         .Union(from rc in db.Set<BigonRoleClaim>()
                                join ur in db.Set<BigonUserRole>() on rc.RoleId equals ur.RoleId
                                where ur.UserId == userId && rc.ClaimValue.Equals("1")
                                select rc.ClaimType)
                         .Distinct()
                         .ToArrayAsync(cancellationToken);


            foreach (var claimName in claims)
            {
                request.Identity.AddClaim(new Claim(claimName, "1"));
            }
        }
    }
}
