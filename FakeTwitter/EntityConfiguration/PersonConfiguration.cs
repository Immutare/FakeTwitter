using FakeTwitter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FakeTwitter.EntityConfiguration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            //                                              //PROPERTIES
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.At)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            //                                              //INDEXES
            HasIndex(p => p.At)
                .IsUnique();

            HasIndex(p => p.Email)
                .IsUnique();

            //                                              //RELATIONSHIPS
            HasMany(p => p.Tweets)
                .WithRequired(t => t.Person)
                .HasForeignKey(t => t.PersonId);

            HasRequired(p => p.ApplicationUser)
                .WithRequiredDependent(appUser => appUser.Person)
                .Map(p => p.MapKey("UserId"));
        }
    }
}