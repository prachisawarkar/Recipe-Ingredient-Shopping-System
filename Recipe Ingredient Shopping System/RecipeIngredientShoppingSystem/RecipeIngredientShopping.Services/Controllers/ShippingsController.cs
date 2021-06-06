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
using RecipeIngredientShoppingSystem.DataEntity;

namespace RecipeIngredientShopping.Services.Controllers
{
    public class ShippingsController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/Shippings
        public List<Shipping> GetShippings()
        {
            return db.Shippings.ToList();
        }

        // GET: api/Shippings/5
        [ResponseType(typeof(Shipping))]
        public IHttpActionResult GetShipping(int id)
        {
            List<Shipping> lshipping = db.Shippings.ToList();
            List<OrderIngredient> lorder = db.OrderIngredients.ToList();

            /*var query = from shipping in lshipping
                        where shipping.CustomerId == id
                        select shipping;*/
            var query = db.Shippings
                     .Where(m => m.UserID == id)
                     .OrderByDescending(m => m.ShippingNumber)
                     .Take(1);

            /*Recipe recipe = db.Recipes.Find(id);*/
            if (query == null)
            {
                return NotFound();
            }
            return Ok(query);
        }

        // PUT: api/Shippings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShipping(int id, Shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipping.ShippingNumber)
            {
                return BadRequest();
            }

            db.Entry(shipping).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingExists(id))
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

        // POST: api/Shippings
        [ResponseType(typeof(Shipping))]
        public IHttpActionResult PostShipping(Shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shippings.Add(shipping);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shipping.ShippingNumber }, shipping);
        }

        // DELETE: api/Shippings/5
        [ResponseType(typeof(Shipping))]
        public IHttpActionResult DeleteShipping(int id)
        {
            Shipping shipping = db.Shippings.Find(id);
            if (shipping == null)
            {
                return NotFound();
            }

            db.Shippings.Remove(shipping);
            db.SaveChanges();

            return Ok(shipping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShippingExists(int id)
        {
            return db.Shippings.Count(e => e.ShippingNumber == id) > 0;
        }
    }
}