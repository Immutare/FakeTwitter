using FakeTwitter.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FakeTwitter.Core
{
    public class Initializer : MigrateDatabaseToLatestVersion<FakeTwitterContext, Configuration>
    {

    }
}