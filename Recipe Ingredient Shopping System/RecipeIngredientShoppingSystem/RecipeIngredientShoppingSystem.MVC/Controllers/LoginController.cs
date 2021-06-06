using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;
using RecipeIngredientShoppingSystem.DataEntity;

namespace RecipeIngredientShoppingSystem.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin model)
        {
            using (var context = new Sprint1Entities())
            {
                bool isValid = context.UserLogins.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {
                   UserLogin admin= context.UserLogins.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
                    AuthenticationProperties options = new AuthenticationProperties();



                    options.AllowRefresh = true;
                    options.IsPersistent = true;
                    //options.ExpiresUtc = DateTime.UtcNow.AddSeconds(token.ExpiresIn);



                    var claims = new[]
                   {
                    new Claim(ClaimTypes.Name, model.UserName.ToString()),



                    new Claim(ClaimTypes.Role, admin.Role),
                     };
                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                    Request.GetOwinContext().Authentication.SignIn(options, identity);
                    if (admin.Role=="admin")

                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("ListOfRecipes", "CustomerMVC");
                    }
                    
                }

                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }

        }

       
        public ActionResult Signup(int id = 0)
        {
            
            if (id == 0)
                return View(new UserLogin());
            return View();

        }


        [HttpPost]
        public ActionResult Signup(UserLogin userLogin)
        {
            try
            {
                if (userLogin.UserId == 0)
                {
                    
                    if(userLogin.MobileNumber.Length>10 || userLogin.MobileNumber.Length > 10)
                    {
                        TempData["ErrorMessage"] = "Please enter a 10-digit mobile number.";
                        return View();
                    }
                    if(!userLogin.Email.Contains("@") && !userLogin.Email.Contains("."))
                    {
                        TempData["ErrorMessage"] = "Please enter a valid email id.";
                        return View();
                    }
                    if(userLogin.Password.Length<6)
                    {
                        TempData["ErrorMessage"] = "Password must be atleast 6 characters long.";
                        return View();
                    }
                    if (String.IsNullOrEmpty(userLogin.Address) && String.IsNullOrEmpty(userLogin.MobileNumber) && String.IsNullOrEmpty(userLogin.UserName) && String.IsNullOrEmpty(userLogin.Password) && String.IsNullOrEmpty(userLogin.Role) && String.IsNullOrEmpty(userLogin.SecurityQuestion) && String.IsNullOrEmpty(userLogin.SecurityAnswer) && String.IsNullOrEmpty(userLogin.Email))
                    {
                        TempData["ErrorMessage"] = "Do not leave any field blank. ";
                        return View();
                    }
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("UserLogin", userLogin).Result;
                    TempData["SuccessMessage"] = "Signup successful";
                }

            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }

            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }
}