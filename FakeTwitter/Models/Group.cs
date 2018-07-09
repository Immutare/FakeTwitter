using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.Models
{
    public class Group
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Name { get; set; }
        // public DateTime Date { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual List<Person> People { get; set; }
    }
}