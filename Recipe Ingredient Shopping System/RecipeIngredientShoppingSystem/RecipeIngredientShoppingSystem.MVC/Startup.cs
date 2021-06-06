using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipeIngredientShoppingSystem.MVC.Startup))]
namespace RecipeIngredientShoppingSystem.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
