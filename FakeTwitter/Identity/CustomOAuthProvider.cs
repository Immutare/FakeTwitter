using FakeTwitter.Core;
using FakeTwitter.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FakeTwitter.Identity
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = context.OwinContext.Get<FakeTwitterContext>()
                .Users.FirstOrDefault(u => u.UserName == context.UserName);
            if (!
                context.OwinContext.Get<TwitterUserManager>()
                .CheckPassword(user, context.Password)
                )
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, ApplicationUser user)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            
            var dbcontext = new FakeTwitterContext();
            var getRolesUsers = dbcontext.Roles.SelectMany(r => r.Users).Where(ur => ur.UserId == user.Id);

            
            var getRoles = dbcontext.Roles.Join(
                getRolesUsers,
                r => r.Id,
                ru => ru.RoleId,
                (qRole, qRoleUser) =>
                new
                {
                    UserId = qRoleUser.UserId,
                    RoleId = qRole.Id,
                    RoleName = qRole.Name
                }
                ).Where( roluser => roluser.UserId == user.Id);
            
            /*
            foreach (var role in getRoles)
            {
                System.Console.WriteLine(String.Format("User: {0}\tRoleId: {1}\tRoleName: {2}", role.UserId, role.RoleId, role.RoleName));
            }
            */

            //                                              //Exception
            //                                              //The entity type IdentityUser is not part of the model for
            //                                              //      the current context.
            // var userRoles = context.OwinContext.Get<TwitterUserManager>().GetRoles(user.Id);
            foreach (var role in getRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            }

            return identity;
        }
    }
}