//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FortuneTellerMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FavoriteColor
    {
        public FavoriteColor()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int FavoriteColorID { get; set; }

        [Display(Name="Favorite Color")]
        public string FavoriteColor1 { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
