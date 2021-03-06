//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecipeIngredientShoppingSystem.DataEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cart
    {
        [Display(Name = "Cart ID")]
        public int CartId { get; set; }
        [Required]
        [Display(Name = "User ID")]
        [RegularExpression("[0-9]+")]
        public Nullable<int> UserID { get; set; }
        [Required]
        [Display(Name = "Ingredient ID")]
        [RegularExpression("[0-9]+")]
        public Nullable<int> IngredientID { get; set; }
        [Required]
        [Display(Name = "Ingredient Quantity")]
        //[RegularExpression("[0-9]+")]
        public Nullable<decimal> IngredientQuantity { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public Nullable<decimal> Amount { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
