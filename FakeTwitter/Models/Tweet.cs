using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Models
{
    public class Tweet
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Likes { get; set; }
        public DateTime DatePublished { get; set; }
        public string Text { get; set; }
        public string Images { get; set; }

        public int PersonId { get; set; }
        public int? ResponseId { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual Person Person { get; set; }
        public virtual List<Tweet> Responses { get; set; }
        public virtual Tweet InResponseTo { get; set; }
    }
}