using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using FakeTwitter.Models;
using FakeTwitter.EntityConfiguration;

namespace FakeTwitter.Core
{
    public class FakeTwitterContext: IdentityDbContext<ApplicationUser>
    {

        public DbSet<Group> Groups { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public FakeTwitterContext() : base("FakeTwitterConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //-------------------------------------------------------------------------------------------------------------
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new TweetConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}