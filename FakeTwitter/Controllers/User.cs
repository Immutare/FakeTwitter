using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Controllers
{
    public class User
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Name { get; set; }
        public string At { get; set; }
        public string Photo { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual Group Groups { get; set; }
        public virtual List<Tweet> Tweets { get; set; }
    }
}