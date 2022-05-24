using Datex.DeliveryConformation.Shared.Enums;
using Datex.DeliveryConformation.Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Datex.DeliveryConformation.Db.Models
{
    [Index(nameof(Id))]
    [Index(nameof(DeliveryTruckId), nameof(Status))]
    public class Shipment : BaseModel, IShipment
    {
        public string? OriginName { get; set; }
        public string? OriginAddress { get; set; }
        public string? DestinationName { get; set; }
        public string? DestinationAddress { get; set; }
        public int NumberOfPackeges { get; set; }
        public string? TrackingNumber { get; set; }
        public ShipmentStatuses Status { get; set; }
        public bool? WasCustomerAtHome { get; set; }
        public bool? WasPackageDamaged { get; set; }
        public string? Notes { get; set; }

        public Guid DeliveryTruckId { get; set; }
        [JsonIgnore]
        public DeliveryTruck DeliveryTruck { get; set; }
    }
}
