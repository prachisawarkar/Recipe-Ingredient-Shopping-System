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
    public class IngredientsControllerTests
    {
        IngredientsController ingredients = new IngredientsController();
        [TestMethod()]
        public void GetIngredientsTest()
        {

            var result = ingredients.GetIngredients();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetIngredientTest()
        {
            int id = 10;
            //Act

            var result = ingredients.GetIngredient(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutIngredientTest()
        {
            int id = 10;
            //Arrange
            Ingredient ingredient = new Ingredient
            {
                IngredientId = 1,
                Name = "Salt",
                RecipeId = 1,
                Price = 10,
                ManufacturerName = "tata",
                Description = "sour",
                ManufacturerDate = new DateTime(2021 - 04 - 04),
                ExpiryDate = new DateTime(2021 - 04 - 04)
            };
            //Act
            var result = ingredients.PutIngredient(id, ingredient);
            //Assert
            Assert.IsNotNull(result);
        }



        [TestMethod()]
        public void DeleteIngredientTest()
        {
            int id = 10;
            var result = ingredients.DeleteIngredient(id);
            Assert.IsNotNull(result);
        }
    }
}