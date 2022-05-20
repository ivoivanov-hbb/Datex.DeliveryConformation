using Datex.DeliveryConformation.Db;
using Datex.DeliveryConformation.Shared;
using Datex.DeliveryConformation.Shared.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Datex.DeliveryConformation.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryTrucksController : ControllerBase
    {
        private readonly ILogger<DeliveryTrucksController> logger;
        private readonly DeliveryConformationDbContext context;

        public DeliveryTrucksController(ILogger<DeliveryTrucksController> logger, DeliveryConformationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IDeliveryTruck>> Get()
        {
            try
            {
                IDeliveryTruck result = await context.DeliveryTrucks.FirstOrDefaultAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}