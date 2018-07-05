using FakeTwitter.Models;
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
            //_userManager = new UserManager<UserModel>(new UserStore<UserModel>(_ctx));
        }
        
        public TwitterUserManager(UserStore<IdentityUser> userStore_I) : base(userStore_I)
        {

        }
        
    }
}