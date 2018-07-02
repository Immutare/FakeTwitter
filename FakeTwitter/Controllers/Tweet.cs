using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Controllers
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
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual User User { get; set; }
        public virtual Tweet Response { get; set; }
    }
}