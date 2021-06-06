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
    public class ShippingsControllerTests
    {
        ShippingsController shippings = new ShippingsController();
        [TestMethod()]
        public void GetShippingsTest()
        {
            var result = shippings.GetShippings();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetShippingTest()
        {
            int id = 10;
            //Act
            var result = shippings.GetShipping(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutShippingTest()
        {
            int id = 10;
            Shipping shipping = new Shipping
            {
                ShippingNumber = 1,
                UserID = 5,
                OrderId = 1,
                ExpectedDeliveryDate = new DateTime(2021, 05, 14)

            };
            var result = shippings.PutShipping(id, shipping);

            //Assert
            Assert.IsNotNull(result);
        }



        [TestMethod()]
        public void DeleteShippingTest()
        {
            int id = 10;
            var result = shippings.DeleteShipping(id);
            Assert.IsNotNull(result);
        }
    }
}