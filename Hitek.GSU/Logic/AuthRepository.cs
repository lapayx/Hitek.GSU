using Hitek.GSU.Logic.Database;
using Hitek.GSU.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Hitek.GSU.Logic
{
    public class AuthRepository : IDisposable
    {
        private Entities _ctx;


        private AppUserManager _userManager;

        bool disposableContext = false;
        public AuthRepository(Entities dbcontext, AppUserManager userManager)
        {
            _ctx = dbcontext;
            _userManager = userManager;
        }

        public AuthRepository()
        {

            _ctx = DIConfig.container.GetInstance<Entities>();
            _userManager = DIConfig.container.GetInstance<AppUserManager>();

        }

        public AuthRepository(AppUserManager _userManager)
        {
            _ctx = new Entities();
            this._userManager = _userManager;

            disposableContext = true;
        }
        /*
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }
        */
        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = null;
#if DEBUG
             user = await _userManager.FindByNameAsync(userName);

#else
            var separateUsername = userName.ToLower().Split('\\');
            if (separateUsername.Length == 2)
            {
                string domen = char.ToUpper((separateUsername[0])[0])+ separateUsername[0].Substring(1);

                MembershipProvider membersip = Membership.Providers["ADMembershipProvider"+ domen];
               
                if (membersip !=null & membersip.ValidateUser(separateUsername[1], password))
                {

                    user = _userManager.FindByName(userName);
                    if (user == null)
                    {
                        // return SignInStatus.Failure;
                        ApplicationUser newAccount = new ApplicationUser()
                        {
                            UserName = userName,
                            Email = $"{domen}-{separateUsername[1]}@gsu.unibel.by"


                        };
                        var result = await _userManager.CreateAsync(newAccount);
                        user = _userManager.FindByName(userName);
                    }
                }
            }
            else
            {
                user = await _userManager.FindAsync(userName, password);
            }
#endif

            return user;
        }
        public Client FindClient(string clientId)
        {
            var client = _ctx.Client.Find(clientId);

            return client;
        }


        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return _ctx.SaveChanges() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public async Task RefreshLastLoginDate(string userName) {
            await _userManager.UpdateLastLoginDateAsync(userName);
        }
        public async Task UpdateLastLoginDateAsync(long userId)
        {
            await _userManager.UpdateLastLoginDateAsync(userId);
        }

        public List<RefreshToken> UpdateLastLoginDateAsync()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            if (disposableContext)
            {
                ((IDisposable)_ctx).Dispose();
            }
        }
    }
}