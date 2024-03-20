using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<UserSubscriptionPlan> UserSubscriptionPlans { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentFile> ContentFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Creator", NormalizedName = "Creator", ConcurrencyStamp = "2" },
               new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Audience", NormalizedName = "Audience", ConcurrencyStamp = "3" }
           );

            modelBuilder.Entity<SubscriptionPlan>().HasData(
                new SubscriptionPlan() { Id = Guid.NewGuid(), Details = "Basic plan", MonthlyAmount = 3.99, YearlyAmount= 39.99 }
                );

        }
    }
}
