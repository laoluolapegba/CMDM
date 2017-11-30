using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Microsoft.Owin.Security.Facebook;
using CMdm.UI.Web.Models;
using CMdm.Data.Identity.Models;

namespace CMdm.UI.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "000000004C16A2F1",
            //    clientSecret: "kwGZwvCb4Q93CIcPOI2TFGJMf-hvoEOi");

            //app.UseTwitterAuthentication(
            //   consumerKey: "4eyQyJHttjp1FYU2zJASHGSnN",
            //   consumerSecret: "qzLPHEkUbx6sWXwbzX6O9TwclpEGHmoSDgjU0Y5hPM66bCMAsc");
            //var x = new FacebookAuthenticationOptions();
            //x.Scope.Add("email");
            //x.AppId = "1623927957873204";
            //x.AppSecret = "7d8d72b7ae4b28016ebdc07c2cd5cab9";
            //x.Provider = new FacebookAuthenticationProvider()
            //{
            //    OnAuthenticated = async context =>
            //    {
            //        //Get the access token from FB and store it in the database and
            //        //use FacebookC# SDK to get more information about the user
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:name", context.Name));
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:email", context.Email));
            //    }
            //};
            //x.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            //app.UseFacebookAuthentication(appId: "1623927957873204",
            //   appSecret: "7d8d72b7ae4b28016ebdc07c2cd5cab9");
            ////appId: "1623927957873204",
            ////  appSecret: "7d8d72b7ae4b28016ebdc07c2cd5cab9"

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "23844072472-mg508pushqma175kg37t9ggagbo5iisq.apps.googleusercontent.com",
            //    ClientSecret = "qYxspJIvbR78FuT0TePMEOJ-"
            //});
            // https://developers.facebook.com/apps 
            //https://console.developers.google.com/
            //https://account.live.com/developers/applications/index
            //https://apps.twitter.com
            //http://www.mysampleapp.com/signin-microsoft.
        }
    }

}