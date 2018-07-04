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

namespace FakeTwitter.Controllers
{
    public class GroupsController : ApiController
    {
        private FakeTwitterContext db = new FakeTwitterContext();

        // GET: api/Groups
        public IQueryable<Group> GetGroups(
            //                                              //OPTIONAL PARAMETERS

            //                                              //FILTERS
            string Name = null, 
            //                                              //POPULATE NAVIGATORS
            bool People = false
            )
        {
            //                                              //Assemblable query
            IQueryable<Group> groupqueryFinalQuery = db.Groups;

            //                                              //FILTERS 
            //                                              //Filter by the name
            if (
                //                                          //The string to filter can not be null
                Name != null && 
                //                                          //The string to filter can not be empty either
                Name != ""
                )
                groupqueryFinalQuery = groupqueryFinalQuery.Where(g => g.Name.Contains(Name));
            //                                              //POPULATES (using Eager loading)
            //                                              //Populate people from group
            if (People)
                groupqueryFinalQuery = groupqueryFinalQuery.Include(g => g.People);

            return groupqueryFinalQuery;
        }

        // GET: api/Groups/5
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> GetGroup(
            //                                              //REQUIRED Id to do the search
            int id,
            //                                              //POPULATE NAVIGATORS
            bool People = false
            )
        {
            //                                              //Assemblable query
            IQueryable<Group> groupqueryFinalQuery = db.Groups.Where(g => g.Id == id);

            //                                              //POPULATES
            //                                              //Populates the people from a group
            if (People)
                groupqueryFinalQuery.Include(g => g.People);

            Group group = await groupqueryFinalQuery.FirstAsync();

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGroup(
            //                                              //Required Id for the update
            int id, 
            //                                              //Required Group object to update
            Group group
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.Id)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> PostGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groups.Add(group);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = group.Id }, group);
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        public async Task<IHttpActionResult> DeleteGroup(int id)
        {
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            await db.SaveChangesAsync();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.Id == id) > 0;
        }
    }
}