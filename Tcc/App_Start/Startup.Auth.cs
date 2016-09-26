﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;
using Mvc5Project.Models;
using Owin;
using System;
using System.Configuration;

namespace Mvc5Project
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
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
            #region Microsoft

            var microsoftAuthenticationOptions = new MicrosoftAccountAuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings["MsI"],
                ClientSecret = ConfigurationManager.AppSettings["MsS"],
                Provider = new MicrosoftAccountAuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("MicrosoftAccessToken", context.AccessToken));
                        foreach (var claim in context.User)
                        {
                            var claimType = string.Format("urn:microsoft:{0}", claim.Key);
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Microsoft"));
                        }
                    }
                }
            };
            microsoftAuthenticationOptions.Scope.Add("wl.basic");
            microsoftAuthenticationOptions.Scope.Add("wl.emails");
            microsoftAuthenticationOptions.Scope.Add("wl.birthday");
            app.UseMicrosoftAccountAuthentication(microsoftAuthenticationOptions);

            #endregion Microsoft

          

            #region Facebook
            var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings["FaceI"],
                AppSecret = ConfigurationManager.AppSettings["FaceS"],
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                    }
                }
            };
            facebookAuthenticationOptions.Scope.Add("public_profile");
            facebookAuthenticationOptions.Scope.Add("email");
            facebookAuthenticationOptions.Scope.Add("user_birthday");
            app.UseFacebookAuthentication(facebookAuthenticationOptions);


            #endregion Facebook

            #region Google

            var googleAuthenticationOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings["GglI"],
                ClientSecret = ConfigurationManager.AppSettings["GglS"],
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("GoogleAccessToken", context.AccessToken));
                        foreach (var claim in context.User)
                        {
                            var claimType = string.Format("urn:google:{0}", claim.Key);
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Google"));
                        }
                    }
                }
            };
            googleAuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.login email");
            app.UseGoogleAuthentication(googleAuthenticationOptions);

            #endregion Google


        }
    }
}
