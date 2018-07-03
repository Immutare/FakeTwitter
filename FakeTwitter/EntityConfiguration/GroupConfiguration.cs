using FakeTwitter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FakeTwitter.EntityConfiguration
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            //                                                  //PROPERTIES
            Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255);
            //                                                  //RELATIONSHIPS
            HasMany(g => g.People)
                .WithRequired(p => p.Group)
                .HasForeignKey(p => p.GroupId);
            
        }
    }
}