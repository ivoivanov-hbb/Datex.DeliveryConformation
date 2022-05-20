using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Shared.Interfaces.Models
{
    public interface IDeliveryTruck
    {
        Guid Id { get; }
        string LicenseNumber { get; }
    }
}
