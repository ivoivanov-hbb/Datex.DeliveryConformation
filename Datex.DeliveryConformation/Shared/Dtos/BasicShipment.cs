using Datex.DeliveryConformation.Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Shared.Dtos
{
    public class BasicShipment : IBasicShipment
    {
        public Guid Id { get; set; }

        public string DestinationName { get; set; }

        public string DestinationAddress { get; set; }

        public int NumberOfPackeges { get; set; }
    }
}
