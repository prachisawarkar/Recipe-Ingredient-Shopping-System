using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeIngredientShopping.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeIngredientShopping.Services;
using RecipeIngredientShoppingSystem.DataEntity;

namespace RecipeIngredientShopping.Services.Controllers.Tests
{
    [TestClass()]
    public class CartRepositoriesControllerTests
    {
        CartRepositoriesController carts = new CartRepositoriesController();
        [TestMethod()]
        public void GetCartsTest()
        {
            var result = carts.GetCartRepositories();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetCartTest()
        {
            int id = 10;
            //Act
            var result = carts.GetCartRepository(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutCartTest()
        {
            int id = 10;
            //Arrange
            CartRepository cart = new CartRepository
            {
                CartId = 1,
                UserID = 5,
                IngredientID = 1,
                IngredientQuantity = 1,
                Amount = 12
            };
            //Act
            var result = carts.PutCartRepository(id, cart);

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void DeleteCartTest()
        {
            int id = 10;
            var result = carts.DeleteCartRepository(id);
            Assert.IsNotNull(result);
        }
    }
}