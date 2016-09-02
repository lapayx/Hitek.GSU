using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Hitek.GSU.Models;
using Microsoft.Owin.Security.DataProtection;
using System.Web.Mvc;
using Microsoft.Owin.Security.OAuth;
using Hitek.GSU.Logic.Providers;
using Hitek.GSU.Logic;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;

namespace Hitek.GSU
{
    public partial class Startup
    {

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuthWebApi(IAppBuilder app, Logic.AuthRepository applicationOAuthProvider)

        {
          var tt = DependencyResolver.Current.GetService<AppUserManager>();

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                //Provider = DependencyResolver.Current.GetService<ApplicationOAuthProvider>(),
               // AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //Provider = new SimpleAuthorizationServerProvider(),
                Provider = new ApplicationOAuthProvider(new Logic.AuthRepository(tt),tt),
               RefreshTokenProvider = new SimpleRefreshTokenProvider(new Logic.AuthRepository(tt)),
                AllowInsecureHttp = true,
                AccessTokenFormat = new CustomJwtFormat("http://localhost:13340")
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            ConfigureOAuthTokenConsumption(app);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = "http://localhost:13340";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }
    }

}