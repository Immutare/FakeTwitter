using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTwitter.App_Start;

namespace FakeTwitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}