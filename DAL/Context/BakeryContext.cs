using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class BakeryContext : IdentityDbContext<BakeryUser>,IDisposable
    {
        public BakeryContext()
            : base("", throwIfV1Schema: false)


        {
        } 

        
        public DbSet<ImageModel> images { get; set; }
        public DbSet<Bakery> Bakerys { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<LogUser> logs { get; set; }
        public DbSet<Introduction> introduction { get; set; }
        public DbSet<Sales> sales { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Shop> shops { get; set; }
        public DbSet<Bill> bill { get; set; }
        public DbSet<Billdetails> billdetails { get; set; }
        public DbSet<News> news { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("QLBB");

            modelBuilder.Entity<BakeryUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<ImageModel>().ToTable("Image");
            
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Bakery>().ToTable("Bakery");
            modelBuilder.Entity<LogUser>().ToTable("Log");
            modelBuilder.Entity<Introduction>().ToTable("Introduction");
            modelBuilder.Entity<Sales>().ToTable("Sale");
            modelBuilder.Entity<Slider>().ToTable("Slider");
            modelBuilder.Entity<Shop>().ToTable("Shop");
           
            modelBuilder.Entity<Bill>().ToTable("Bill");
            modelBuilder.Entity<Billdetails>().ToTable("Billdetails");
            modelBuilder.Entity<News>().ToTable("News");

        }
        public static BakeryContext Create()
        {
            return new BakeryContext();
        }

    }
}
