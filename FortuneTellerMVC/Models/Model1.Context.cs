﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FortuneTellerMVCEntities1 : DbContext
    {
        public FortuneTellerMVCEntities1()
            : base("name=FortuneTellerMVCEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BirthMonth> BirthMonths { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FavoriteColor> FavoriteColors { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
