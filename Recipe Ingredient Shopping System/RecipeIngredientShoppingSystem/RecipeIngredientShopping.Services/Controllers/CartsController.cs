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
using RecipeIngredientShoppingSystem.Exceptions;

namespace RecipeIngredientShopping.Services.Controllers
{
    public class CartsController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/Carts
        public IHttpActionResult GetCarts()
        {
            List<Cart> lcarts = db.Carts.ToList();
            var query = from cart in lcarts
                        select cart;
            return Ok(query);
        }

        // GET: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult GetCart(int id)
        {
            List<Cart> lcarts = db.Carts.ToList();
            var query = from c in lcarts
                        where c.UserID == id
                        select c;
            /*Cart cart = db.Carts.Find(id);*/
            if (query == null)
            {
                return NotFound();
            }
            return Ok(query);
        }

        // PUT: api/Carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCart(int id, Cart cart)
        {
            cart = new Cart
            {
                CartId = cart.CartId,
                UserID = cart.UserID,
                IngredientID = cart.IngredientID,
                IngredientQuantity = cart.IngredientQuantity,
                Amount = cart.Amount
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.CartId)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PostCart(Cart cart)
        {
            /*cart = new Cart
            {
                CartId = cart.CartId,
                UserID = cart.UserID,
                IngredientID = cart.IngredientID,
                IngredientQuantity = cart.IngredientQuantity,
                Amount = cart.Amount
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carts.Add(cart);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cart.CartId }, cart);*/

            try
            {
                cart = new Cart
                {
                    CartId = cart.CartId,
                    UserID = cart.UserID,
                    IngredientID = cart.IngredientID,
                    IngredientQuantity = cart.IngredientQuantity,
                    Amount = cart.Amount
                };
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Carts.Add(cart);
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new RecipeIngredientSystemExceptions();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtRoute("DefaultApi", new { id = cart.CartId }, cart);
        }
    

        // DELETE: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            Cart cart = db.Carts.Find(id);
            try
            {
                if (cart == null)
                {
                    return NotFound();
                }

                db.Carts.Remove(cart);
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new RecipeIngredientSystemExceptions();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int id)
        {
            return db.Carts.Count(e => e.CartId == id) > 0;
        }
    }
}