﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCSE_BACKEND.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SCSE_DBEntities : DbContext
    {
        public SCSE_DBEntities()
            : base("name=SCSE_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<BankInformation> BankInformation { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ImgPortfolio> ImgPortfolio { get; set; }
        public virtual DbSet<OrganizationConfiguration> OrganizationConfiguration { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<Portfolio> Portfolio { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<PhotoGallery> PhotoGallery { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<FieldName> FieldName { get; set; }
        public virtual DbSet<LoginRole> LoginRole { get; set; }
    }
}
