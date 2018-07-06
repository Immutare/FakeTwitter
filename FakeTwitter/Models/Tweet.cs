using FakeTwitter.ViewModels;
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
        //                                                  //COMPUTED PROPERTIES
        //                                                  //https://daveaglick.com/posts/computed-properties-and-entity-framework
        //                                                  //http://www.entityframeworktutorial.net/code-first/notmapped-dataannotations-attribute-in-code-first.aspx
        //-------------------------------------------------------------------------------------------------------------
        public Like[] Likes_Z { get { return Like.ParseLikes(this.Likes); } }
        public string[] Images_Z { get { return TweetJsonFormat.ParseImages(this.Images); } }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
        public virtual Person Person { get; set; }
        public virtual List<Tweet> Responses { get; set; }
        public virtual Tweet InResponseTo { get; set; }

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //RELATIONSHIPS
        //-------------------------------------------------------------------------------------------------------------
    }
}