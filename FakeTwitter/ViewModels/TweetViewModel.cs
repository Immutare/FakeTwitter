using FakeTwitter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FakeTwitter.ViewModels
{
    //-----------------------------------------------------------------------------------------------------------------
    public class TweetViewModel
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
        //                                                  //CONSTRUCTORS
        //-------------------------------------------------------------------------------------------------------------
        public TweetViewModel()
        {

        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public TweetViewModel(Tweet tweetTweet_I)
        {
            if (tweetTweet_I == null)
                return;

            this.Id = tweetTweet_I.Id;
            this.Likes = tweetTweet_I.Likes;
            this.DatePublished = tweetTweet_I.DatePublished;
            this.Text = tweetTweet_I.Text;
            this.Images = tweetTweet_I.Images;
            this.PersonId = tweetTweet_I.PersonId;
            this.ResponseId = tweetTweet_I.ResponseId;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public TweetViewModel(string strJson_I)
        {
            if (string.IsNullOrWhiteSpace(strJson_I))
                return;

            TweetViewModel tweetViewModel = JsonConvert.DeserializeObject<TweetViewModel>(strJson_I);

            this.Id = tweetViewModel.Id;
            this.Likes = tweetViewModel.Likes;
            this.DatePublished = tweetViewModel.DatePublished;
            this.Text = tweetViewModel.Text;
            this.Images = tweetViewModel.Images;
            this.PersonId = tweetViewModel.PersonId;
            this.ResponseId = tweetViewModel.ResponseId;
        }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //METHODS
        //-------------------------------------------------------------------------------------------------------------
        public Tweet ToTweet()
        {
            return new Tweet
            {
                Id = this.Id,
                Likes = this.Likes,
                DatePublished = this.DatePublished,
                Text = this.Text,
                Images = this.Images,
                PersonId = this.PersonId,
                ResponseId = this.ResponseId
            };
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public TweetJsonFormat ToJsonFormat()
        {
            return new TweetJsonFormat(this);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------
    public class TweetJsonFormat
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public Like[] Likes { get; set; }
        public DateTime DatePublished { get; set; }
        public string Text { get; set; }
        public string[] Images { get; set; }
        public int PersonId { get; set; }
        public int? ResponseId { get; set; }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS
        //-------------------------------------------------------------------------------------------------------------
        public TweetJsonFormat()
        {

        }

        public TweetJsonFormat(TweetViewModel tweetViewModel)
        {
            this.Id = tweetViewModel.Id;
            this.DatePublished = tweetViewModel.DatePublished;
            this.Text = tweetViewModel.Text;
            this.PersonId = tweetViewModel.PersonId;
            this.ResponseId = tweetViewModel.ResponseId;
            
            this.Likes = Like.parseLikes(tweetViewModel.Likes);
            //                                              //Porque se usó pipe?
            //                                              //https://www.seroundtable.com/google-pipes-in-urls-20735.html
            this.Images = TweetJsonFormat.ParseImages(tweetViewModel.Images);
        }
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //METHODS
        //-------------------------------------------------------------------------------------------------------------
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string[] ParseImages(string strImagesFromTweet_I)
        {
            if (string.IsNullOrWhiteSpace(strImagesFromTweet_I))
                return null;

            return strImagesFromTweet_I.Split('|');
        }

    }
    //-----------------------------------------------------------------------------------------------------------------
    public class Like
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //PROPERTIES
        //-------------------------------------------------------------------------------------------------------------
        public int PersonId { get; set; }
        public DateTime Date { get; set; }

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //METHODS
        //-------------------------------------------------------------------------------------------------------------
        public Like()
        {

        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Like(int intPersonId_I, DateTime dateDate_I)
        {
            this.PersonId = intPersonId_I;
            this.Date = dateDate_I;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public Like(string intPersonId_I, string dateDate_I)
        {
            int intAux = 0;

            Int32.TryParse(intPersonId_I, out intAux);

            this.PersonId = intAux;
            this.Date = DateTime.Parse(dateDate_I);
        }

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //METHODS
        //-------------------------------------------------------------------------------------------------------------
        public static Like[] parseLikes(string strLikesToParse_I)
        {
            List<Like> darrLikes = new List<Like>();
            List<string> strPersonAndDate = strLikesToParse_I.Split('&').ToList();

            foreach (string strLikeData in strPersonAndDate)
            {
                string[] arrstrSplitData = strLikeData.Split('|');

                if (arrstrSplitData.Length % 2 != 0)
                    return null;

                darrLikes.Add(new Like(arrstrSplitData[0], arrstrSplitData[1]));
            }

            return darrLikes.ToArray();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------
}