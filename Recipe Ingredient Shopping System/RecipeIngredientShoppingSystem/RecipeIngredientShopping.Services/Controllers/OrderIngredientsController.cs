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
    public class OrderIngredientsController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/OrderIngredients
        public IQueryable<OrderIngredient> GetOrderIngredients()
        {
            return db.OrderIngredients;
        }

        // GET: api/OrderIngredients/5
        [ResponseType(typeof(OrderIngredient))]
        public IHttpActionResult GetOrderIngredient(int id)
        {
            OrderIngredient orderIngredient = db.OrderIngredients.Find(id);
            if (orderIngredient == null)
            {
                return NotFound();
            }

            return Ok(orderIngredient);
        }

        // PUT: api/OrderIngredients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderIngredient(int id, OrderIngredient orderIngredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderIngredient.OrderId)
            {
                return BadRequest();
            }

            db.Entry(orderIngredient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderIngredientExists(id))
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

        // POST: api/OrderIngredients
        [ResponseType(typeof(OrderIngredient))]
        public IHttpActionResult PostOrderIngredient(OrderIngredient orderIngredient)
        {
            orderIngredient = new OrderIngredient
            {
                UserID = orderIngredient.UserID,
                DateOfOrder = orderIngredient.DateOfOrder,
                MobileNumber = orderIngredient.MobileNumber,
                DeliveryAddress = orderIngredient.DeliveryAddress,
                TotalBillAmount = orderIngredient.TotalBillAmount,
                NoOfIngredients = orderIngredient.NoOfIngredients,
                GrandTotal = orderIngredient.GrandTotal
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderIngredients.Add(orderIngredient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderIngredient.OrderId }, orderIngredient);
        }

        // DELETE: api/OrderIngredients/5
        [ResponseType(typeof(OrderIngredient))]
        public IHttpActionResult DeleteOrderIngredient(int id)
        {
            OrderIngredient orderIngredient = db.OrderIngredients.Find(id);
            if (orderIngredient == null)
            {
                return NotFound();
            }

            db.OrderIngredients.Remove(orderIngredient);
            db.SaveChanges();

            return Ok(orderIngredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderIngredientExists(int id)
        {
            return db.OrderIngredients.Count(e => e.OrderId == id) > 0;
        }
    }
}