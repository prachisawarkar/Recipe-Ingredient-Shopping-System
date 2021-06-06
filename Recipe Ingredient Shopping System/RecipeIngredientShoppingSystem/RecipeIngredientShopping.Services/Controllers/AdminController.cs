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
    public class UserLoginController : ApiController
    {
        private Sprint1Entities db = new Sprint1Entities();

        // GET: api/UserLogin
        public IQueryable<UserLogin> GetUserLogins()
        {
            return db.UserLogins;
        }

        // GET: api/UserLogin/5
        [ResponseType(typeof(UserLogin))]
        public IHttpActionResult GetUserLogin(int id)
        {
            UserLogin UserLogin = db.UserLogins.Find(id);
            if (UserLogin == null)
            {
                return NotFound();
            }

            return Ok(UserLogin);
        }

        // PUT: api/UserLogin/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserLogin(int id, UserLogin UserLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != UserLogin.UserId)
            {
                return BadRequest();
            }

            db.Entry(UserLogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(id))
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

        // POST: api/UserLogin
        [ResponseType(typeof(UserLogin))]
        public IHttpActionResult PostUserLogin(UserLogin UserLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.UserLogins.Add(UserLogin);
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
            return CreatedAtRoute("DefaultApi", new { id = UserLogin.UserId }, UserLogin);
        }

        // DELETE: api/UserLogin/5
        [ResponseType(typeof(UserLogin))]
        public IHttpActionResult DeleteUserLogin(int id)
        {
            UserLogin UserLogin = db.UserLogins.Find(id);
            if (UserLogin == null)
            {
                return NotFound();
            }

            db.UserLogins.Remove(UserLogin);
            db.SaveChanges();

            return Ok(UserLogin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserLoginExists(int id)
        {
            return db.UserLogins.Count(e => e.UserId == id) > 0;
        }
    }
}