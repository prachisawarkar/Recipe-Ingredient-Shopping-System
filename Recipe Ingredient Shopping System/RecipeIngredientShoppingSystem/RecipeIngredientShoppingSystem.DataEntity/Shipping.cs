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

    public partial class Shipping
    {
        [Display(Name = "Shipping Number")]
        public int ShippingNumber { get; set; }
        [Required]
        [RegularExpression("[0-9]+")]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        [Required]
        [RegularExpression("[0-9]+")]
        [Display(Name = "User ID")]
        public Nullable<int> UserID { get; set; }
        [Required]
        [Display(Name = "Expected Delivery Date")]
        public System.DateTime ExpectedDeliveryDate { get; set; }
    
        public virtual OrderIngredient OrderIngredient { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
