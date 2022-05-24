using Datex.DeliveryConformation.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Shared.Interfaces.Models
{
    public interface IShipment : IBasicShipment
    {
        string? OriginName { get; }
        string? OriginAddress { get; }
        string? TrackingNumber { get; }
        ShipmentStatuses Status { get; set; }
        bool? WasCustomerAtHome { get; set; }
        bool? WasPackageDamaged { get; set; }
        string? Notes { get; set; }
    }
}
