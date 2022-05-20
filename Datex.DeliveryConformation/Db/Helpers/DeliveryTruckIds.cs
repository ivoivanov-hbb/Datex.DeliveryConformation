using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.Db.Helpers
{
    // This is only for puspose of adding fake data
    // In real life those must not be seeded and added from the UI or other migration from another source
    internal class DeliveryTruckIds
    {
        public static List<Guid> Ids = new List<Guid>()
        {
            new Guid("B11CEABB-7B65-4216-832D-A1C3309A6AA5"),
            new Guid("F9C3A27D-C25A-4FE3-80BB-EA562FFA7BC7"),
            new Guid("DAF6F1FE-7F99-4567-BB22-CA36F44C79E8")
        };
    }
}
