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
using RecipeIngredientShoppingSystem.MVC.Models;
using ToeknManager;

namespace RecipeIngredientShoppingSystem.MVC.Controllers
{
    public class CustomerMVCController : Controller
    {
        // GET: CustomerMVC
       
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "customer,admin")]
        public ActionResult ListOfRecipes()
        {
            IEnumerable<Recipe> recipeList = null;
            try
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Recipes").Result; //calling the web api controller "Recipes"
                var JsonContent = response.Content.ReadAsStringAsync().Result;
                recipeList = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(JsonContent); //Deserializing json string
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return View(recipeList);
        }
        //view ingredients
        public ActionResult ViewIngredients(int id = 0)
        {
            int UserId = 0;
            if (id == 0)
            {
                
                return View(new Ingredient());
            }
            else
            {
                string Username = User.Identity.Name;
                using (Sprint1Entities db = new Sprint1Entities())
                {

                    UserId = (from user in db.UserLogins


                                     where user.UserName == Username
                                     select user.UserId).FirstOrDefault();
                    //return UserRoles;
                }
                ViewBag.UserId = UserId;
                IEnumerable<Ingredient> ingredientList = null;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Ingredients/" + id.ToString()).Result; //calling the web api controller "Recipes"
                var JsonContent = response.Content.ReadAsStringAsync().Result;
                ingredientList = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(JsonContent); //Deserializing json string
                return View(ingredientList);
                /*HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Recipes/" + id.ToString()).Result; //calling the web api controller "Recipes"
                return View(response.Content.ReadAsAsync<MVCRecipeModel>().Result);*/
            }
        }

        //GET - add to cart
        public ActionResult AddToCart(int userid, int ingredientid)
        {
            //sending request to find web api REST service resource GetCustomer and GetIngredients using HttpClient
            HttpResponseMessage responseFromCustomer = GlobalVariables.WebApiClient.GetAsync("CartRepositories/" + userid.ToString()).Result; //calling the web api controller "Recipes"
            HttpResponseMessage responseFromIngredient = GlobalVariables.WebApiClient.GetAsync("Ingredients/" + ingredientid.ToString()).Result; //calling the web api controller "Recipes"

            var response = responseFromCustomer.Content.ReadAsAsync<AddToCartUserModel>().Result;
            return View(response);
            /*var JsonContent = response.Content.ReadAsStringAsync().Result;
            return View(JsonContent);*/

        }
        [HttpPost]
        public ActionResult AddToCart(AddToCartUserModel cart)
        {
            try
            {
                if (cart.CartId == 0)
                {
                    if (cart.IngredientQuantity.HasValue)
                    {

                        if (cart.IngredientQuantity <= 0)
                        {
                            TempData["ErrorMessage"] = "Enter a valid quantity";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Enter a valid quantity";
                        return View();
                    }
                    cart.Amount = cart.IngredientQuantity * cart.Price;
                    //sending request to find web api REST service 
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Carts", cart).Result;

                    TempData["SuccessMessage"] = "Cart Saved";

                    //checking the response is successful or not which is sent using HttpClient
                    if (response.IsSuccessStatusCode)
                    {
                        //storing the response details received from web api
                        var addToCartResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                else
                {
                    cart.Amount = cart.IngredientQuantity * cart.Price;
                    //sending request to find web api REST service 
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Carts/" + cart.CartId, cart).Result;
                    //checking the response is successful or not which is sent using HttpClient
                    if (response.IsSuccessStatusCode)
                    {
                        //storing the response details received from web api
                        var addToCartResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            //returning the cart list to view
            /*return RedirectToAction("ViewCarts", new RouteValueDictionary(
                new {"CustomerMVC", "ViewCarts", id = cart.UserID });*/
            return RedirectToAction("ViewCarts", new { userId = cart.UserID });
        }
        //Delete Cart
        public ActionResult DeleteCart(int cartid, int userid)
        {
            try
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Carts/" + cartid.ToString()).Result; //calling the web api controller "Recipes"
                TempData["SuccessMessage"] = "Cart Deleted";
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ViewCarts", new { userId = userid });
        }

        //get list of carts
        public ActionResult ViewCarts(int userId)
        {
            int UserId= 0;
            string Username = User.Identity.Name;
            using (Sprint1Entities db = new Sprint1Entities())
            {

                UserId = (from user in db.UserLogins


                          where user.UserName == Username
                          select user.UserId).FirstOrDefault();
                //return UserRoles;
            }
            ViewBag.UserId = UserId;
            IEnumerable<Cart> cartList = null;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Carts/" + userId.ToString()).Result; //calling the web api controller "Recipes"
            var JsonContent = response.Content.ReadAsStringAsync().Result;
            cartList = JsonConvert.DeserializeObject<IEnumerable<Cart>>(JsonContent); //Deserializing json string
            return View(cartList);
        }

        //add order
        public ActionResult PlaceOrder(int userid)
        {
            return View(new OrderIngredient());
        }
        [HttpPost]
        public ActionResult PlaceOrder(OrderIngredient order)
        {
            try
            {
                order.DateOfOrder = DateTime.Now;
                /*order.OrderId = Convert.ToInt32(Request["OrderId"]);*/
                order.TotalBillAmount = Convert.ToDecimal(Request.QueryString["tamount"]);
                order.NoOfIngredients = Convert.ToInt32(Request.QueryString["tcount"]);
                order.GrandTotal = Convert.ToDecimal(Request.QueryString["gtotal"]);

                //sending request to find web api REST service 
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("OrderIngredients", order).Result;

                TempData["SuccessMessage"] = "Order Placed";

                //checking the response is successful or not which is sent using HttpClient
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Order Placed Successfully!";
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("ViewShippingDetails", new { userID = order.UserID });
        }

        //get list of carts
        public ActionResult ViewShippingDetails(int userID)
        {
            IEnumerable<Shipping> shipppingList = null;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Shippings/" + userID.ToString()).Result; //calling the web api controller "Recipes"
            var JsonContent = response.Content.ReadAsStringAsync().Result;
            shipppingList = JsonConvert.DeserializeObject<IEnumerable<Shipping>>(JsonContent); //Deserializing json string
            return View(shipppingList);
        }
    }
}