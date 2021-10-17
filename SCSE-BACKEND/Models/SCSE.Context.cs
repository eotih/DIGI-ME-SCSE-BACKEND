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
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BankInformation> BankInformations { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentGallery> DocumentGalleries { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<ImgPortfolio> ImgPortfolios { get; set; }
        public virtual DbSet<NewsEN> NewsENs { get; set; }
        public virtual DbSet<NewsVN> NewsVNs { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostsEN> PostsENs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<VideoGallery> VideoGalleries { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<LoginRole> LoginRoles { get; set; }
        public virtual DbSet<SlugNew> SlugNews { get; set; }
    }
}
