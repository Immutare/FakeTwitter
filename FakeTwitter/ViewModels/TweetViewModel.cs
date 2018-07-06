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
            Tweet tweetToReturn = new Tweet();

            tweetToReturn.Id = this.Id;
            tweetToReturn.Likes = TweetJsonFormat.ProcessLikesFromString(this.Likes);
            tweetToReturn.DatePublished = this.DatePublished;
            tweetToReturn.Text = this.Text;
            tweetToReturn.Images = TweetJsonFormat.ProcessIncomingArrayOfImages(this.Images);
            tweetToReturn.PersonId = this.PersonId;
            tweetToReturn.ResponseId = this.ResponseId;

            return tweetToReturn;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        
        
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
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public TweetJsonFormat(TweetViewModel tweetViewModel)
        {
            this.Id = tweetViewModel.Id;
            this.DatePublished = tweetViewModel.DatePublished;
            this.Text = tweetViewModel.Text;
            this.PersonId = tweetViewModel.PersonId;
            this.ResponseId = tweetViewModel.ResponseId;

            // this.Likes = tweetViewModel.Likes;
            // this.Images = tweetViewModel.Images;
            this.Likes = Like.ParseLikes(tweetViewModel.Likes);
            
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

            //                                              //Porque se usó pipe?
            //                                              //https://www.seroundtable.com/google-pipes-in-urls-20735.html
            return strImagesFromTweet_I.Split('|');
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string StrImagesToJson(string strImagesFromTweet_I)
        {
            if (string.IsNullOrWhiteSpace(strImagesFromTweet_I))
                return null;

            string[] arrstrImages = TweetJsonFormat.ParseImages(strImagesFromTweet_I);

            return JsonConvert.SerializeObject(arrstrImages);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static string ProcessIncomingArrayOfImages(string strImagesFromTweet_I)
        {
            if (string.IsNullOrWhiteSpace(strImagesFromTweet_I))
                return null;

            string[] arrstrImages = JsonConvert.DeserializeObject<string[]>(strImagesFromTweet_I);

            System.Diagnostics.Debug.WriteLine("IMAGENES!!!!!!!!!!!!!!");
            foreach(string strImage in arrstrImages)
            {
                System.Diagnostics.Debug.WriteLine(strImage);
            }

            return TweetJsonFormat.ImagesToStr(arrstrImages);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string ImagesToStr(string[] arrstrImages_I)
        {
            string strfinalString = arrstrImages_I.Aggregate((i, j) => i + "|" + j);

            return strfinalString;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string ProcessLikesFromString(string strLikes_I)
        {
            Like[] arrlikeLikes = JsonConvert.DeserializeObject<Like[]>(strLikes_I);

            return TweetJsonFormat.LikesToStr(arrlikeLikes);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string LikesToStr(Like[] likeLikes_I)
        {
            List<string> darrstrLikes = new List<string>();

            foreach (Like like in likeLikes_I)
            {
                string strLike = string.Format("{0}|{1}", like.PersonId.ToString(), like.Date.ToString());

                System.Diagnostics.Debug.WriteLine(strLike);

                darrstrLikes.Add(strLike);
            }

            string strfinalString = darrstrLikes.Aggregate((i, j) => i + "&" + j);

            return strfinalString;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
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
        public static Like[] ParseLikes(string strLikesToParse_I)
        {
            if (string.IsNullOrWhiteSpace(strLikesToParse_I))
                return null;

            List<Like> darrLikes = new List<Like>();

            //                                              //Splits the different likes by the letter &
            List<string> strPersonAndDate = strLikesToParse_I.Split('&').ToList();

            //                                              //Iterates the array to construct and append each Like 
            //                                              //      object.
            foreach (string strLikeData in strPersonAndDate)
            {
                //                                          //Splits the PersonId and the Date (in that order)
                string[] arrstrSplitData = strLikeData.Split('|');

                //                                          //If the split is not even then returns a null
                if (arrstrSplitData.Length % 2 != 0)
                    return null;

                //                                          //Sends two strings to the constructor wich parses each 
                //                                          //      string
                darrLikes.Add(new Like(arrstrSplitData[0], arrstrSplitData[1]));
            }

            return darrLikes.ToArray();
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string LikesToJson(string strLikesToParse_I)
        {
            if (string.IsNullOrWhiteSpace(strLikesToParse_I))
                return null;

            Like[] likeParsedLikes = Like.ParseLikes(strLikesToParse_I);

            return JsonConvert.SerializeObject(likeParsedLikes);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------
}