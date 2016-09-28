using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Hitek.GSU.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hitek.GSU.Logic.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        AuthRepository _repoAuthRepository;
        private readonly string _publicClientId;
        private AppUserManager _userManager;

        public ApplicationOAuthProvider(AuthRepository _repoAuthRepository, AppUserManager _userManager)
        {
            this._repoAuthRepository = _repoAuthRepository;
            this._userManager = _userManager;


        }

        public ApplicationOAuthProvider()
        {
            //this._repoAuthRepository = new AuthRepository();

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            /* var userManager = context.OwinContext.GetUserManager<AppUserManager>();

             ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

             if (user == null)
             {
                 context.SetError("invalid_grant", "The user name or password is incorrect.");
                 return;
             }

             ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                OAuthDefaults.AuthenticationType);
             ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                 CookieAuthenticationDefaults.AuthenticationType);

             AuthenticationProperties properties = CreateProperties(user.UserName);
             AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
             context.Validated(ticket);
             context.Request.Context.Authentication.SignIn(cookiesIdentity);*/


            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });


            ApplicationUser user = await _repoAuthRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            /*
        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        identity.AddClaim(new Claim("sub", context.UserName));
        identity.AddClaim(new Claim("role", "user"));

        */

            ClaimsIdentity identity = await user.GenerateUserIdentityAsync(_userManager, "JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            // identity.AddClaim(new Claim("sub", context.UserName));
            // identity.AddClaim(new Claim("role", "user"));

            var props = new Dictionary<string, string>();
            props.Add("as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId);
            props.Add("userName", context.UserName);
            props.Add("role", String.Join(",", identity.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .ToArray()));
   
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties(props));
            context.Validated(ticket);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }



        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }


            client = _repoAuthRepository.FindClient(context.ClientId);


            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == Models.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }


    }



    public class Helper
    {
        public static string GetHash(string input)
        {
            System.Security.Cryptography.HashAlgorithm hashAlgorithm = new System.Security.Cryptography.SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}