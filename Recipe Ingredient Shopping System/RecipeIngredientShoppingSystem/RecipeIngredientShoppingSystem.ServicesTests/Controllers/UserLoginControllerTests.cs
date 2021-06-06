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
    public class UserLoginControllerTests
    {
        UserLoginController users = new UserLoginController();
        [TestMethod()]
        public void GetUserLoginsTest()
        {
            var result = users.GetUserLogins();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetUserLoginTest()
        {
            int id = 10;
            //Act
            var result = users.GetUserLogin(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutUserLoginTest()
        {
            int id = 10;
            UserLogin user = new UserLogin()
            {
                UserId = 5,
                UserName = "sakshi",
                Password = "sakshi123",
                Role = "admin",
                MobileNumber = "1234567890",
                Address = "abcd",
                Email = "sakshi@gmail.com",
                SecurityQuestion = "college",
                SecurityAnswer = "muj"
            };
            var result = users.PutUserLogin(id, user);

            //Assert
            Assert.IsNotNull(result);
        }



        [TestMethod()]
        public void DeleteUserLoginTest()
        {
            int id = 10;
            var result = users.DeleteUserLogin(id);
            Assert.IsNotNull(result);
        }
    }
}