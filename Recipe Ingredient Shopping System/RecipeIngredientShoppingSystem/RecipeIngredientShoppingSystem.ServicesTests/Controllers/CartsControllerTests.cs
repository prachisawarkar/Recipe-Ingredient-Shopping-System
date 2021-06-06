using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeIngredientShopping.Services.Controllers;
using RecipeIngredientShoppingSystem.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeIngredientShopping.Services.Controllers.Tests
{
    [TestClass()]
    public class CartsControllerTests
    {
        CartsController carts = new CartsController();
        [TestMethod()]
        public void GetCartsTest()
        {
            var result = carts.GetCarts();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetCartTest()
        {
            int id = 10;
            //Act
            var result = carts.GetCart(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutCartTest()
        {
            int id = 10;
            //Arrange
            Cart cart = new Cart
            {
                CartId = 1,
                UserID = 5,
                IngredientID = 1,
                IngredientQuantity = 2,
                Amount = 20
            };
            //Act
            var result = carts.PutCart(id, cart);

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void DeleteCartTest()
        {
            int id = 10;
            var result = carts.DeleteCart(id);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetCartsTest1()
        {

        }
    }
}