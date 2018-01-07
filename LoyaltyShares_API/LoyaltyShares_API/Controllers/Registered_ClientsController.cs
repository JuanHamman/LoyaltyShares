using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LoyaltyShares_API.DB;

namespace LoyaltyShares_API.Controllers
{
    public class Registered_ClientsController : ApiController
    {
        private LoyaltySharesDBEntities db = new LoyaltySharesDBEntities();

        // GET: api/Registered_Clients
        public IQueryable<Registered_Clients> GetRegistered_Clients()
        {
            return db.Registered_Clients;
        }

        // GET: api/Registered_Clients/5
        [ResponseType(typeof(Registered_Clients))]
        public IHttpActionResult GetRegistered_Clients(int id)
        {
            Registered_Clients registered_Clients = db.Registered_Clients.Find(id);
            if (registered_Clients == null)
            {
                return NotFound();
            }

            return Ok(registered_Clients);
        }

        // PUT: api/Registered_Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistered_Clients(int id, Registered_Clients registered_Clients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registered_Clients.ID)
            {
                return BadRequest();
            }

            db.Entry(registered_Clients).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Registered_ClientsExists(id))
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

        // POST: api/Registered_Clients
        [ResponseType(typeof(Registered_Clients))]
        public IHttpActionResult PostRegistered_Clients(Registered_Clients registered_Clients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Registered_Clients.Add(registered_Clients);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registered_Clients.ID }, registered_Clients);
        }

        // DELETE: api/Registered_Clients/5
        [ResponseType(typeof(Registered_Clients))]
        public IHttpActionResult DeleteRegistered_Clients(int id)
        {
            Registered_Clients registered_Clients = db.Registered_Clients.Find(id);
            if (registered_Clients == null)
            {
                return NotFound();
            }

            db.Registered_Clients.Remove(registered_Clients);
            db.SaveChanges();

            return Ok(registered_Clients);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Registered_ClientsExists(int id)
        {
            return db.Registered_Clients.Count(e => e.ID == id) > 0;
        }
    }
}