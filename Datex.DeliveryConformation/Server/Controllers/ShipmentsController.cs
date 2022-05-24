using Datex.DeliveryConformation.Db;
using Datex.DeliveryConformation.Db.Models;
using Datex.DeliveryConformation.Shared.Dtos;
using Datex.DeliveryConformation.Shared.Enums;
using Datex.DeliveryConformation.Shared.Interfaces.Models;
using Datex.DeliveryConformation.Shared.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datex.DeliveryConformation.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentsController : ControllerBase
    {
        private readonly ILogger<ShipmentsController> logger;
        private readonly DeliveryConformationDbContext context;

        public ShipmentsController(ILogger<ShipmentsController> logger, DeliveryConformationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<IBasicShipment>>> GetShipments(Guid deliveryTruckId, ShipmentStatuses? shipmentStatus, int page = 1, int pageSize = 25)
        {
            if(page <= 0 || pageSize <= 0)
            {
                return BadRequest("Bad values for paging. page and pageSize must be >= 1.");
            }

            // If more complex logic is to be included and more models are created, a Mapper can be implemented.
            List<IBasicShipment> result = await context.Shipments
                .Where(s => s.DeliveryTruckId.Equals(deliveryTruckId) 
                            && shipmentStatus.HasValue ? s.Status == shipmentStatus : true)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new BasicShipment() { Id = s.Id, DestinationAddress = s.DestinationAddress, DestinationName = s.DestinationName, NumberOfPackeges = s.NumberOfPackeges })
                .ToListAsync<IBasicShipment>();

            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCount(ShipmentStatuses? shipmentStatus)
        {
            int result = await context.Shipments
                .Where(s => shipmentStatus.HasValue ? s.Status == shipmentStatus : true).CountAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IShipment>> GetShipment(Guid id)
        {
            IShipment result = await context.Shipments.FindAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Update(IShipment shipment)
        {
            if(!ShipmentValidator.IsShipmentValid(shipment))
            {
                return ValidationProblem();
            }

            IShipment result = await context.Shipments.FindAsync(shipment.Id);
            if(result == null)
            {
                return NotFound("No such shipment was found.");
            }

            result.Status = shipment.Status;
            result.WasCustomerAtHome = shipment.WasCustomerAtHome;
            result.WasPackageDamaged = shipment.WasPackageDamaged;
            result.Notes = shipment.Notes;

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
