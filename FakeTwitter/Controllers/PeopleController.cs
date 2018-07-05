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
using Microsoft.AspNet.Identity.EntityFramework;

namespace FakeTwitter.Controllers
{
    public class PeopleController : ApiController
    {
        private FakeTwitterContext db = new FakeTwitterContext();

        // GET: api/People
        // [Authorize]
        public IQueryable<Person> GetPeople(
            //                                              //OPTIONAL PARAMETERS

            //                                              //FILTERS
            //                                              //Filter by Name
            string Name = null,
            //                                              //Filter by @
            string At = null,
            //                                              //Filter by email
            string Email = null,
            //                                              //POPULATE NAVIGATORS
            //                                              //Populate Group
            bool Group = false,
            //                                              //Populate Tweets
            bool Tweets = false,
            //                                              //Tweets Size
            int twSize = 25,
            //                                              //Tweets Page
            int twInPage = 1
            )
        {
            IQueryable<Person> peoplequeryableFinalQuery = db.People;

            //                                              //FILTERS
            //                                              //Filter by Name
            if (string.IsNullOrWhiteSpace(Name))
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Where(p => p.Name.Contains(Name));
            //                                              //Filter by At (@)
            if (string.IsNullOrWhiteSpace(At))
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Where(p => p.At.Contains(At));
            //                                              //Filter by Email
            if (string.IsNullOrWhiteSpace(Email))
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Where(p => p.Email.Contains(Email));
            //                                              //POPULATES
            //                                              //Populate group
            if (Group)
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Include(p => p.Group);
            //                                              //Populate tweets
            if (Tweets)
            {
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Include(
                    p => p.Tweets
                    .Skip((twInPage - 1) * twSize)
                    .Take(twSize)
                    );
            }


            return peoplequeryableFinalQuery;
        }

        // GET: api/People/5
        // [Authorize]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetPerson(
            //                                              //REQUIRED Id for the search
            int id,
            //                                              //POPULATE NAVIGATORS
            //                                              //Populate Group
            bool Group = false,
            //                                              //Populate Tweets
            bool Tweets = false,
            //                                              //Tweets Size
            int twSize = 25,
            //                                              //Tweets Page
            int twInPage = 1
            )
        {
            IQueryable<Person> peoplequeryableFinalQuery = db.People.Where(p => p.Id == id);
            //                                              //POPULATES
            //                                              //Populate group
            if (Group)
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Include(p => p.Group);
            //                                              //Populate tweets
            if (Tweets)
            {
                peoplequeryableFinalQuery = peoplequeryableFinalQuery.Include(
                    p => p.Tweets
                    .Skip((twInPage - 1) * twSize)
                    .Take(twSize)
                    );
            }

            Person person = await peoplequeryableFinalQuery.FirstAsync();

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        // [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [AllowAnonymous]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person, [FromBody] string hashPass = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (hashPass == null)
            {
                return BadRequest("Password was not found");
            }
            else
            {
                var userRoleId = db.Roles.First(c => c.Name == "User").Id;
                var store = new TwitterUserStore();

                //                                          //      1.- Se crea un Application User
                ApplicationUser appuserNewUser = db.Users.Add(new ApplicationUser(person.At.ToString())
                {
                    Email = person.Email.ToString(),
                    EmailConfirmed = true
                });

                //                                          //      2.- Se le añade el rol deseado
                appuserNewUser.Roles.Add(new IdentityUserRole { RoleId = userRoleId });

                //                                          //      3.- Se hace el store.SetPasswordHashAsync con
                //                                          //          la variable del ApplicationUser
                await store.SetPasswordHashAsync(
                    appuserNewUser,
                    new TwitterUserManager().PasswordHasher.HashPassword(hashPass)
                );

                person.ApplicationUser = appuserNewUser;

                //                                          //      4.- Se hace el context.People.AddOrUpdate pasando
                //                                          //          la relación
                db.People.Add(person);

                //                                          //      5.- context.SaveChanges();
                db.SaveChanges();
                
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = person.Id }, person);
            }
        }

        // DELETE: api/People/5
        /*
        [Authorize]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(int id)
        {
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.Id == id) > 0;
        }
    }
}