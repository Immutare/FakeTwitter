using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Models
{
    public class Person
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Name { get; set; }
        public string At { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }

        public int GroupId { get; set; }
        // public string UserId { get; set; }
        // public int UserId { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual Group Group { get; set; }
        public virtual List<Tweet> Tweets { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public virtual Person Person { get; set; }

        public ApplicationUser(): base()
        {

        }

        public ApplicationUser(string userName_I): base(userName_I)
        {

        }
    }
}