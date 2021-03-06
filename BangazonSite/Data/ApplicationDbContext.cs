﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BangazonSite.Models;

namespace BangazonSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<BangazonSite.Models.Product> Product { get; set; }

        public DbSet<BangazonSite.Models.PaymentType> PaymentType { get; set; }

        public DbSet<BangazonSite.Models.Order> Order { get; set; }

        public DbSet<BangazonSite.Models.ProductType> ProductType { get; set; }

        public DbSet<BangazonSite.Models.OrderProduct> OrderProduct { get; set; }
    }
}
