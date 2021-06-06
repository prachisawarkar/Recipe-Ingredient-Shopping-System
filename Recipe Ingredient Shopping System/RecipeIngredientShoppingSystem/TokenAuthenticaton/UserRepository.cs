using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeIngredientShoppingSystem.DataEntity;
namespace TokenAuthenticaton
{
    public class UserRepository : IDisposable
    {

        Sprint1Entities sprint1Entities = new Sprint1Entities();

        public UserLogin ValidateUser(string username, string password)
        {
            return sprint1Entities.UserLogins.FirstOrDefault(user =>
            user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.Password == password);
        }
        public void Dispose()
        {
            sprint1Entities.Dispose();
        }
    }
}