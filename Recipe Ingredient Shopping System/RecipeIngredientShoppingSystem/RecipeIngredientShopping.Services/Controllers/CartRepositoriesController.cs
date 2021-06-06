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
    public class CartRepositoriesController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/CartRepositories
        public IQueryable<CartRepository> GetCartRepositories()
        {
            return db.CartRepositories;
        }

        // GET: api/CartRepositories/5
        [ResponseType(typeof(CartRepository))]
        public IHttpActionResult GetCartRepository(int id)
        {
            CartRepository cartRepository = db.CartRepositories.Find(id);
            if (cartRepository == null)
            {
                return NotFound();
            }

            return Ok(cartRepository);
        }

        // PUT: api/CartRepositories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCartRepository(int id, CartRepository cartRepository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cartRepository.CartId)
            {
                return BadRequest();
            }

            db.Entry(cartRepository).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartRepositoryExists(id))
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

        // POST: api/CartRepositories
        [ResponseType(typeof(CartRepository))]
        public IHttpActionResult PostCartRepository(CartRepository cartRepository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CartRepositories.Add(cartRepository);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cartRepository.CartId }, cartRepository);
        }

        // DELETE: api/CartRepositories/5
        [ResponseType(typeof(CartRepository))]
        public IHttpActionResult DeleteCartRepository(int id)
        {
            CartRepository cartRepository = db.CartRepositories.Find(id);
            if (cartRepository == null)
            {
                return NotFound();
            }

            db.CartRepositories.Remove(cartRepository);
            db.SaveChanges();

            return Ok(cartRepository);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartRepositoryExists(int id)
        {
            return db.CartRepositories.Count(e => e.CartId == id) > 0;
        }
    }
}