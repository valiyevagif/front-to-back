using Bigon.Infrastructure.Entities.Membership;
using Bigon.Infrastructure.Enums;
using Bigon.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bigon.Infrastructure.Services.Concrates
{
    public interface IUserManager
    {
        Task<string> GenerateRefreshTokenAsync(BigonUser user, string accessToken, CancellationToken cancellationToken = default);
        Task<BigonUser> GetUserById(int userId, CancellationToken cancellationToken = default);
        Task<bool> ValidateRefreshTokenAsync(BigonUser user, string refreshToken, CancellationToken cancellationToken = default);
    }
    public class AppUserManager : UserManager<BigonUser>, IUserManager
    {
        private readonly ICryptoService cryptoService;
        private readonly IServiceProvider services;

        public AppUserManager(ICryptoService cryptoService, IUserStore<BigonUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<BigonUser> passwordHasher, IEnumerable<IUserValidator<BigonUser>> userValidators, IEnumerable<IPasswordValidator<BigonUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<BigonUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.cryptoService = cryptoService;
            this.services = services;
        }

        public async Task<string> GenerateRefreshTokenAsync(BigonUser user, string accessToken, CancellationToken cancellationToken = default)
        {
            string refreshToken = cryptoService.ToSha1($"{user.Id}{accessToken}{DateTime.Now:hh:mm:ss}");

            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();

                var tokenTable = db.Set<BigonUserToken>();

                var token = new BigonUserToken
                {
                    UserId = user.Id,
                    LoginProvider = "REFRESH_TOKEN",
                    Name = "REFRESH_TOKEN",
                    Value = cryptoService.ToMd5(refreshToken),
                    Type = TokenType.RefreshToken,
                    ExpireDate = DateTime.UtcNow.AddDays(15)
                };

                await tokenTable.AddAsync(token, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
            }

            return refreshToken;
        }

        public async Task<BigonUser> GetUserById(int userId, CancellationToken cancellationToken = default)
        {
            var user = await Users.FirstOrDefaultAsync(m => m.Id == userId, cancellationToken);
            return user;
        }

        public async Task<bool> ValidateRefreshTokenAsync(BigonUser user, string refreshToken, CancellationToken cancellationToken = default)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();

                var token = await db.Set<BigonUserToken>().Where(m => m.UserId == user.Id
                && m.Type == TokenType.RefreshToken && m.ExpireDate != null)
                    .OrderByDescending(m => m.ExpireDate)
                    .FirstOrDefaultAsync(cancellationToken);


                if (token != null && token.ExpireDate >= DateTime.UtcNow && token.Value.Equals(refreshToken))
                {
                    token.ExpireDate = DateTime.UtcNow;
                    await db.SaveChangesAsync(cancellationToken);
                    return true;
                }
            }

            return false;
        }
    }
}
