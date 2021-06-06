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
    public class RecipesController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/Recipes
        public IHttpActionResult GetRecipes()
        {
            List<Recipe> lrecipe = db.Recipes.ToList();
            var query = from recipe in lrecipe
                        select recipe;
            if (query == null)
            {
                return NotFound();
            }

            return Ok(query);
        }

        // GET: api/Recipes/5
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult GetRecipe(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        // PUT: api/Recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipe(int id, Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            db.Entry(recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult PostRecipe(Recipe recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Recipes.Add(recipe);
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
            return CreatedAtRoute("DefaultApi", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipes/5
        [ResponseType(typeof(Recipe))]
        public IHttpActionResult DeleteRecipe(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            try
            {
                if (recipe == null)
                {
                    return NotFound();
                }

                db.Recipes.Remove(recipe);
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
            return Ok(recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipeExists(int id)
        {
            return db.Recipes.Count(e => e.RecipeId == id) > 0;
        }
    }
}