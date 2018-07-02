using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Controllers
{
    public class Group
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Name { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual List<User> Users { get; set; }
    }
}