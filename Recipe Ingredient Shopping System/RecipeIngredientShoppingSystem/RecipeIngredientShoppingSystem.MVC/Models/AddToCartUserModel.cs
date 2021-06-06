using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeIngredientShoppingSystem.MVC.Models
{
    public class AddToCartUserModel
    {
        [Key]
        public int CartId { get; set; }
        public int IngredientId { get; set; }
        public int? UserID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? IngredientQuantity { get; set; }
        public decimal? Amount { get; set; }
    }
}