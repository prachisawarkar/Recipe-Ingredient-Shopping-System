using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RecipeIngredientShoppingSystem.DataEntity;
using ToeknManager;

namespace RecipeIngredientShoppingSystem.MVC.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult ListRecipes()
        {
            IEnumerable<Recipe> recipeList = null;
            try
            {
                using (var client = new HttpClient())
                {
                    Token token = ClsTokenMgr.GetAccessToken("sakshi", "sakshi123");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                    client.BaseAddress = new Uri("https://localhost:44300/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Recipes").Result;


                    if (responseTask.IsSuccessStatusCode)
                    {
                        var JsonContent = responseTask.Content.ReadAsStringAsync().Result;
                        recipeList = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(JsonContent);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        recipeList = Enumerable.Empty<Recipe>();
                        ModelState.AddModelError("Unauthorized", "Please contact system administrator");
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return View(recipeList);
        }

        public ActionResult ListIngredients()
        {
            IEnumerable<Ingredient> ingredientList = null;
            try
            {
                using (var client = new HttpClient())
                {
                    Token token = ClsTokenMgr.GetAccessToken("sakshi", "sakshi123");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                    client.BaseAddress = new Uri("https://localhost:44300/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Ingredients").Result;


                    if (responseTask.IsSuccessStatusCode)
                    {
                        var JsonContent = responseTask.Content.ReadAsStringAsync().Result;
                        ingredientList = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(JsonContent);
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        ingredientList = Enumerable.Empty<Ingredient>();
                        ModelState.AddModelError("Unauthorized", "Please contact system administrator");
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return View(ingredientList);
        }

       
        public ActionResult AddOrEditIngredient(int id = 0)
        {
            Ingredient ingredient = null;            
            if (id == 0)
                return View(new Ingredient());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Ingredients/" + id.ToString()).Result;
                var listIngredient = response.Content.ReadAsAsync<List<Ingredient>>().Result;
                    if(listIngredient!=null)
                {
                    ingredient = listIngredient.FirstOrDefault();
                }
                return View(ingredient);
            }

        }


        [HttpPost]
        public ActionResult AddOrEditIngredient(Ingredient ing)
        {
            try
            {
                if (ing.IngredientId == 0)
                {
                    if (ing.ManufacturerDate.HasValue)
                    {
                        TimeSpan diff = (DateTime)ing.ExpiryDate - (DateTime)ing.ManufacturerDate;
                        if (diff.Days == 0)
                        {
                            TempData["ErrorMessage"] = "Expiry date cannot be same as Manufacture date";
                            return View();
                        }
                    }
                    if (ing.ManufacturerDate.HasValue)
                    {
                        TimeSpan diff = (DateTime)ing.ExpiryDate - (DateTime)ing.ManufacturerDate;
                        if (diff.Days < 0)
                        {
                            TempData["ErrorMessage"] = "Expiry date cannot be before Manufacture date";
                            return View();
                        }
                    }
                    if(String.IsNullOrEmpty(ing.Name) && String.IsNullOrEmpty(ing.Description) && ing.RecipeId== null && ing.Price== null && String.IsNullOrEmpty(ing.ManufacturerName) && !ing.ManufacturerDate.HasValue && !ing.ExpiryDate.HasValue)
                    {
                        TempData["ErrorMessage"] = "Do not leave any field blank. ";
                        return View();
                    }
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Ingredients", ing).Result;
                    TempData["SuccessMessage"] = "Ingredient Added Successfully";
                }
                else
                {
                    if (ing.ManufacturerDate.HasValue)
                    {
                        TimeSpan diff = (DateTime)ing.ExpiryDate - (DateTime)ing.ManufacturerDate;
                        if (diff.Days == 0)
                        {
                            TempData["ErrorMessage"] = "Expiry date cannot be same as Manufacture date";
                            return View();
                        }
                    }
                    if (ing.ManufacturerDate.HasValue)
                    {
                        TimeSpan diff = (DateTime)ing.ExpiryDate - (DateTime)ing.ManufacturerDate;
                        if (diff.Days < 0)
                        {
                            TempData["ErrorMessage"] = "Expiry date cannot be before Manufacture date";
                            return View();
                        }
                    }
                    if (String.IsNullOrEmpty(ing.Name) && String.IsNullOrEmpty(ing.Description) && ing.RecipeId == null && ing.Price == null && String.IsNullOrEmpty(ing.ManufacturerName) && !ing.ManufacturerDate.HasValue && !ing.ExpiryDate.HasValue)
                    {
                        TempData["ErrorMessage"] = "Do not leave any field blank. ";
                        return View();
                    }
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Ingredients/" + ing.IngredientId, ing).Result;
                    TempData["SuccessMessage"] = "Ingredient Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ListIngredients");
        }
        public ActionResult DeleteIngredient(int id)
        {
            try
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Ingredients/" + id.ToString()).Result;
                TempData["SuccessMessage"] = "Deleted Successfully";
            }
            
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ListIngredients");
        }
        public ActionResult AddOrEditRecipe(int id = 0)
        {
            if (id == 0)
                return View(new Recipe());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Recipes/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Recipe>().Result);
            }
        }


        [HttpPost]
        public ActionResult AddOrEditRecipe(Recipe recipe)
        {
            try
            {
                if (recipe.RecipeId == 0)
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Recipes", recipe).Result;
                    TempData["SuccessMessage"] = "Recipe Added Successfully";
                }
                else
                {
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Recipes/" + recipe.RecipeId, recipe).Result;
                    TempData["SuccessMessage"] = "Recipe Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ListRecipes");
            //return View();
        }

        public ActionResult DeleteRecipe(int id)
        {
            try
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Recipes/" + id.ToString()).Result;
                TempData["SuccessMessage"] = "Recipe Deleted Successfully";
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ListRecipes");
        }

        public ActionResult ListOrders()
        {
            IEnumerable<OrderIngredient> orderList = null;
            using (var client = new HttpClient())
            {
                Token token = ClsTokenMgr.GetAccessToken("sakshi", "sakshi123");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                client.BaseAddress = new Uri("https://localhost:44300/api/");
                //HTTP GET
                var responseTask = client.GetAsync("OrderIngredients").Result;


                if (responseTask.IsSuccessStatusCode)
                {
                    var JsonContent = responseTask.Content.ReadAsStringAsync().Result;
                    orderList = JsonConvert.DeserializeObject<IEnumerable<OrderIngredient>>(JsonContent);
                }
                else //web api sent error response 
                {
                    //log response status here..

                    orderList = Enumerable.Empty<OrderIngredient>();
                    ModelState.AddModelError("Unauthorized", "Please contact system administrator");
                }
            }
            return View(orderList);
        }
        public ActionResult ListShipping()
        {
            IEnumerable<Shipping> shippingList = null;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("AdminShippings").Result; //calling the web api controller "Shipping"
            var JsonContent = response.Content.ReadAsStringAsync().Result;
            shippingList = JsonConvert.DeserializeObject<IEnumerable<Shipping>>(JsonContent); //Deserializing json string
            return View(shippingList);
        }

        public ActionResult EditShipping(int id = 0)
        {
            Shipping shipping = null;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("AdminShippings/" + id.ToString()).Result;
            var listShipping = response.Content.ReadAsAsync<List<Shipping>>().Result;
            if (listShipping != null)
            {
                shipping = listShipping.FirstOrDefault();
            }
            return View(shipping);
        }


        [HttpPost]
        public ActionResult EditShipping(Shipping ship)
        {
            try
            {
                
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("AdminShippings/" + ship.ShippingNumber, ship).Result;
                TempData["SuccessMessage"] = "Shipping Details Updated Successfully";

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ListShipping");
        }

    }
}
