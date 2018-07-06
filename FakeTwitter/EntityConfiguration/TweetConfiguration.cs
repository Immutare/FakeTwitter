using FakeTwitter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FakeTwitter.EntityConfiguration
{
    public class TweetConfiguration : EntityTypeConfiguration<Tweet>
    {
        public TweetConfiguration()
        {
            //                                              //PROPERTIES
            Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(120);

            // Property(t => t.Likes_Z)

            //                                              //RELATIONSHIPS
            HasMany(t => t.Responses)
                .WithOptional(t => t.InResponseTo)
                .HasForeignKey(t => t.ResponseId);
        }
    }
}