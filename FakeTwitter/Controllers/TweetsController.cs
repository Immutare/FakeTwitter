using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FakeTwitter.Core;
using FakeTwitter.Models;
using FakeTwitter.ViewModels;
using Newtonsoft.Json;

namespace FakeTwitter.Controllers
{
    public class TweetsController : ApiController
    {
        private FakeTwitterContext db = new FakeTwitterContext();

        // GET: api/Tweets
        public IQueryable<Tweet> GetTweets(
            //                                              //OPTIONAL PARAMETERS

            //                                              //FILTERS
            //                                              //Filter by Text content
            string Text = null,
            //                                              //Filter by person
            int PersonId = 0,
            //                                              //Filter by @ from person
            string PersonAt = null,
            //                                              //Filter by group
            int GroupId = 0,
            //                                              //POPULATE NAVIGATORS
            //                                              //Populate Person(User), true by default
            bool Person = true,
            //                                              //Populate all responses from a Tweet
            bool Responses = false,
            //                                              //Populate the tweet in response/answered, default true
            bool InResponseTo = true,
            //                                              //Other type of commands
            int Order = 1
            )
        {
            IQueryable<Tweet> tweetsqueryFinalQuery = db.Tweets;

            //                                              //FILTERS
            //                                              //Filter by the containing text
            if (Text != null && Text != "")
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Where(t => t.Text.Contains(Text));
            //                                              //Filter by the id from the person/user
            if (PersonId != 0)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Where(t => t.PersonId == PersonId);
            //                                              //Filter by the @ from the person/user
            if (PersonAt != null && PersonAt != "")
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Where(t => t.Person.At == PersonAt);
            //                                              //Filter by the group
            if (GroupId != 0)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Where(t => t.Person.GroupId == GroupId);

            //                                              //POPULATES
            //                                              //Populates the info from the person/user
            if (Person)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Include(t => t.Person).AsNoTracking();
            //                                              //Populates all the replies of the tweets
            if (Responses)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Include(t => t.Responses).AsNoTracking();
            //                                              //Populate the tweet in response/answered, default true
            if (InResponseTo)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Include(t => t.InResponseTo).AsNoTracking();
            //                                              //Other types of commands
            if (Order > 0)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.OrderBy(t => t.DatePublished);
            else if (Order < 0)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.OrderByDescending(t => t.DatePublished);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string json = JsonConvert.SerializeObject(tweetsqueryFinalQuery.AsNoTracking(), settings);

            return tweetsqueryFinalQuery.AsNoTracking();
        }

        // GET: api/Tweets/5
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> GetTweet(
            int id,                                         //REQUIRED Id for the search
            //                                              //POPULATE NAVIGATORS
            //                                              //Populate Person(User), true by default
            bool Person = true,
            //                                              //Populate all responses from a Tweet
            bool Responses = false,
            //                                              //Populate the tweet in response/answered, default true
            bool InResponseTo = true
            )
        {
            IQueryable<Tweet> tweetsqueryFinalQuery = db.Tweets.Where(t => t.Id == id);
            //                                              //POPULATES
            //                                              //Populates the info from the person/user
            if (Person)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Include(t => t.Person);
            //                                              //Populates all the replies of the tweets
            if (Responses)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Include(t => t.Responses);
            //                                              //Populate the tweet in response/answered, default true
            if (InResponseTo)
                tweetsqueryFinalQuery = tweetsqueryFinalQuery.Include(t => t.InResponseTo);

            Tweet tweet = await tweetsqueryFinalQuery.AsNoTracking().FirstAsync();

            if (tweet == null)
            {
                return NotFound();
            }

            string json = JsonConvert.SerializeObject(tweet);
            System.Console.WriteLine(json);

            return Ok(tweet);
        }

        // PUT: api/Tweets/5
        // [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTweet(int id, Tweet tweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tweet.Id)
            {
                return BadRequest();
            }

            db.Entry(tweet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TweetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tweets
        // [Authorize]
        // [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> PostTweet([FromBody] TweetViewModel tweetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tweets.Add(tweetViewModel.ToTweet());
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tweetViewModel.Id }, tweetViewModel.ToJsonFormat());
        }

        // DELETE: api/Tweets/5
        // [Authorize]
        // [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> DeleteTweet(int id)
        {
            Tweet tweet = await db.Tweets.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }


            db.Tweets.Remove(tweet);
            await db.SaveChangesAsync();

            TweetViewModel tweetViewModel = new TweetViewModel(tweet);

            return Ok(tweetViewModel.ToJsonFormat());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TweetExists(int id)
        {
            return db.Tweets.Count(e => e.Id == id) > 0;
        }
    }
}