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
    public class OrderIngredientsControllerTests
    {
        OrderIngredientsController orders = new OrderIngredientsController();
        [TestMethod()]
        public void GetOrderIngredientsTest()
        {
            var result = orders.GetOrderIngredients();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetOrderIngredientTest()
        {
            int id = 10;
            //Act
            var result = orders.GetOrderIngredient(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutOrderIngredientTest()
        {
            int id = 10;
            OrderIngredient order = new OrderIngredient
            {
                OrderId = 1,
                UserID = 5,
                DateOfOrder = new DateTime(2021, 05, 14),
                MobileNumber = "1234567899",
                DeliveryAddress = "abcd",
                TotalBillAmount = 20,
                NoOfIngredients = 1,
                GrandTotal = (decimal?)23.6
            };
            var result = orders.PutOrderIngredient(id, order);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteOrderIngredientTest()
        {
            int id = 10;
            var result = orders.DeleteOrderIngredient(id);
            Assert.IsNotNull(result);
        }
    }
}