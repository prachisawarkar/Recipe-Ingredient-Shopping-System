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
    public class RecipesControllerTests
    {
        RecipesController recipes = new RecipesController();

        [TestMethod()]
        public void GetRecipesTest()
        {
            int id = 10;
            //Act
            var result = recipes.GetRecipes();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetRecipeTest()
        {
            int id = 10;
            //Act
            var result = recipes.GetRecipe(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PutRecipeTest()
        {
            int id = 10;
            //Arrange
            Recipe recipe = new Recipe
            {
                RecipeId = 1,
                Name = "Pasta",
                Description = "v.good"
            };
            //Act
            var result = recipes.PutRecipe(id, recipe);

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void DeleteRecipeTest()
        {
            int id = 10;
            var result = recipes.DeleteRecipe(id);
            Assert.IsNotNull(result);
        }



    }
}