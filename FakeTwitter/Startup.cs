using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTwitter.App_Start;
using FakeTwitter.Core;
using FakeTwitter.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using Microsoft.Owin.Cors;

namespace FakeTwitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigureOAuth(app);
        }
    }
}