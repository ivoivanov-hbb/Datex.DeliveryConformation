using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Shared.Interfaces.Models
{
    public interface IBasicShipment
    {
        Guid Id { get; }
        string? DestinationName { get; }
        string? DestinationAddress { get; }
        int NumberOfPackeges { get; }
    }
}
