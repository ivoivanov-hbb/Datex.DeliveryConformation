using Datex.DeliveryConformation.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Db.Configurations
{
    internal class DeliveryTruckConfiguration : IEntityTypeConfiguration<DeliveryTruck>
    {
        public void Configure(EntityTypeBuilder<DeliveryTruck> builder)
        {
            List<DeliveryTruck> deliveryTrucks = new List<DeliveryTruck>();
            foreach(Guid id in Helpers.DeliveryTruckIds.Ids)
            {
                deliveryTrucks.Add(new DeliveryTruck() { Id = id, LicenseNumber = id.ToString().Substring(1, 6) });
            }

            builder.HasData(deliveryTrucks);
        }
    }
}
