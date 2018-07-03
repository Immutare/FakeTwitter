using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Core
{
    public class TwitterUserStore : UserStore<IdentityUser>
    {
        public TwitterUserStore() : base(new FakeTwitterContext())
        {

        }
    }
}