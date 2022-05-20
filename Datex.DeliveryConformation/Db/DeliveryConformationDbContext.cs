using Datex.DeliveryConformation.Db.Configurations;
using Datex.DeliveryConformation.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Db
{
    public class DeliveryConformationDbContext : DbContext
    {
        public DeliveryConformationDbContext(DbContextOptions<DeliveryConformationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DeliveryTruckConfiguration());
            modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
        }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<DeliveryTruck> DeliveryTrucks { get; set; }
    }
}
