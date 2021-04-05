using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<CardStatus> CardStatuses { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<PackingMaterial> PackingMaterials { get; set; }
        public DbSet<PackingMaterialUsedOnOrder> PackingMaterialUsedOnOrders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<PickupRequest> PickupRequests { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }        
    }
}
