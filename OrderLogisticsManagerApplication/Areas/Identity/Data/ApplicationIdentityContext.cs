using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using OrderLogisticsManagerApplication.Models.Database.Identity;

namespace OrderLogisticsManagerApplication.Data
{
    public class ApplicationIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardStatus> CardStatuses { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }

        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options)
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
    }
}
