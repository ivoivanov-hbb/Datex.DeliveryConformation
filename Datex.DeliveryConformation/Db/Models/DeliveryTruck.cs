using Datex.DeliveryConformation.Shared.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Db.Models
{
    [Index(nameof(Id))]
    public class DeliveryTruck : BaseModel, IDeliveryTruck
    {
        public string LicenseNumber { get; set; }
        // The truck can be updated daily - to have have new shipments every day
        // The truck can have driver as well - one driver can have one truck at any given time, but one truck can have many drivers for a day
        [JsonIgnore]
        public List<Shipment> Shipments { get; set; }
    }
}
