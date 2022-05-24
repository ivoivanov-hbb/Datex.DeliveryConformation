using Datex.DeliveryConformation.Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Shared.Validation
{
    public class ShipmentValidator
    {
        public static bool IsShipmentValid(IShipment shipment)
        {
            if (shipment == null)
                return false;

            switch (shipment.Status)
            {
                case Enums.ShipmentStatuses.OutForDelivery:
                    // Here another logic can be added for initial shipment creation
                    return true;
                case Enums.ShipmentStatuses.Delivered:
                    return shipment.WasCustomerAtHome.HasValue && shipment.WasPackageDamaged.HasValue;
                case Enums.ShipmentStatuses.HeldInTruck:
                    return !string.IsNullOrWhiteSpace(shipment.Notes);
            }

            throw new Exception("Unknown validation!");
        }
    }
}
