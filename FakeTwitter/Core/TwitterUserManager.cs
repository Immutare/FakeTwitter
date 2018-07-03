using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Core
{
    public class TwitterUserManager: UserManager<IdentityUser>
    {
        public TwitterUserManager() : base(new TwitterUserStore())
        {

        }
    }
}