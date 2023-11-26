using Bigon.Infrastructure.Entities.Membership;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bigon.Infrastructure.Services.Concrates
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions options;

        public JwtService(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }
        public string GenerateAccessToken(BigonUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var token = new JwtSecurityToken(options.Issuer,options.Audience,
              claims,
              expires: DateTime.UtcNow.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
