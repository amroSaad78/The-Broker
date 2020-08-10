using Apartment.API.Infrastructure;
using Apartment.API.IntegrationEvents;
using Apartment.API.IntegrationEvents.Events;
using Apartment.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Apartment.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApartmentController : ControllerBase
    {
        private readonly ApartmentContext _context;
        private readonly IApartmentIntegrationEventService _apartmentIntegrationEventService;
        private readonly ApartmentSettings _settings;

        public ApartmentController(ApartmentContext context, IOptionsSnapshot<ApartmentSettings> settings, IApartmentIntegrationEventService apartmentIntegrationEventService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _apartmentIntegrationEventService = apartmentIntegrationEventService;
            _settings = settings.Value;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // POST api/v1/[controller]/rent
        [HttpPost]
        [Route("rent")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddRent([FromBody] Rent rent, [FromHeader(Name = "x-requestid")] string requestId)
        {
            _context.Rent.Add(rent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddRent), new { id = rent.Id });
        }

        // POST api/v1/[controller]/sale
        [HttpPost]
        [Route("sale")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddSale([FromBody] Sale sale)
        {
            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddSale), new { id = sale.Id });
        }

        // PUT api/v1/[controller]/rent
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateRent([FromBody] Rent rent)
        {
            var oldrent = await _context.Rent.SingleOrDefaultAsync(a => a.Id == rent.Id);
            if (oldrent == null)
            {
                return NotFound(new { Message = $"Apartment rent with id {rent.Id} not found." });
            }
            var oldPrice = oldrent.Price;
            bool raiseApartmentPriceChangedEvent = oldPrice != rent.Price;

            oldrent = rent;
            _context.Rent.Update(oldrent);
            if (raiseApartmentPriceChangedEvent)
            {
                var priceChangedEvent = new RentPriceChangedIntegrationEvent(oldrent.Id, rent.Price, oldPrice);

                await _apartmentIntegrationEventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);

                await _apartmentIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);
            }
            else
            {
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(UpdateRent), new { id = oldrent.Id });
        }
    }
}
