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
    public class IngredientsController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/Ingredients
        public List<Ingredient> GetIngredients()
        {
            return db.Ingredients.ToList();
        }

        // GET: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public List<Ingredient> GetIngredient(int id)
        {
            List<Ingredient> lingredient = db.Ingredients.ToList();
            //var query = from ingredient in lingredient
            //            where ingredient.RecipeId == id
            //            select ingredient.;
            lingredient= lingredient.Where(i => i.RecipeId.Equals(id)).ToList();
            /*Recipe recipe = db.Recipes.Find(id);*/
            //if (query == null)
            //{
            //    return NotFound();
            //}
            return lingredient;
        }

        // PUT: api/Ingredients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIngredient(int id, Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            db.Entry(ingredient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult PostIngredient(Ingredient ingredient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Ingredients.Add(ingredient);
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
            return CreatedAtRoute("DefaultApi", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult DeleteIngredient(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            try
            {
                if (ingredient == null)
                {
                    return NotFound();
                }

                db.Ingredients.Remove(ingredient);
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
            return Ok(ingredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredientExists(int id)
        {
            return db.Ingredients.Count(e => e.IngredientId == id) > 0;
        }
    }
}