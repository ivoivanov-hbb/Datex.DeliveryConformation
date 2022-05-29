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
    internal class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            Random random = new Random();   
            List<Shipment> shipmentList = new List<Shipment>();
            for(int i = 0; i < 1000; i++)
            {
                shipmentList.Add(new Shipment()
                {
                    Id = Guid.NewGuid(),
                    DestinationAddress = $"Fake destination address for {i}",
                    DestinationName = $"Fake destination name for {i}",
                    NumberOfPackeges = random.Next(5) + 1,
                    OriginAddress = $"Fake origin address for {i}",
                    OriginName = $"Fake origin name for {i}",
                    Status = (Shared.Enums.ShipmentStatuses)random.Next(3),
                    TrackingNumber = $"FTN{i}-{i}",
                    WasCustomerAtHome = null,
                    WasPackageDamaged = null,
                    DeliveryTruckId = Helpers.DeliveryTruckIds.Ids[random.Next(3)]
                });
            }

            builder.HasData(shipmentList);
        }
    }
}
