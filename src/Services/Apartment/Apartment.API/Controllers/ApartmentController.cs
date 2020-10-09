using Apartment.API.Infrastructure;
using Apartment.API.Infrastructure.Services;
using Apartment.API.IntegrationEvents;
using Apartment.API.Model;
using BuildingBlocks.IntegrationEventLogEF.Utilities;
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
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApartmentController : ControllerBase
    {        
        private readonly IApartmentIntegrationEventService _apartmentIntegrationEventService;
        private readonly AppSettings _settings;
        private readonly ApartmentContext _context;

        public ApartmentController(IOptionsSnapshot<AppSettings> settings, 
                                    IApartmentIntegrationEventService apartmentIntegrationEventService,                                    
                                    ApartmentContext context)
        {
            _apartmentIntegrationEventService = apartmentIntegrationEventService;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;            
            _settings = settings.Value;
        }

        //-------------TODO Using cosomos db for all types of realestate---------------//

        // POST api/v1/[controller]/rent
        [HttpPost]
        [Route("rent")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddRent([FromBody] Payload<Rent> payload)
        {
            await Save(new Payload<IPayload>(payload.Apartment, payload.InObject));
            return CreatedAtAction(nameof(AddRent), new { id = payload.Apartment.Id });
        }

        
        // POST api/v1/[controller]/sale
        [HttpPost]
        [Route("sale")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddSale([FromBody] Payload<Sale> payload)
        {
            await Save(new Payload<IPayload>(payload.Apartment, payload.InObject));
            return CreatedAtAction(nameof(AddSale), new { id = payload.Apartment.Id });
        }

        private async Task Save(Payload<IPayload> payload)
        {
            await ResilientTransaction.New(_context).ExecuteAsync(async () =>
            {
                _context.Apartment.Add(payload.Apartment);
                await _context.SaveChangesAsync();
                payload.InObject.ApartmentId = payload.Apartment.Id;
                _context.Add(payload.InObject);
                await _context.SaveChangesAsync();
            });
        }

        /*
        // PUT api/v1/[controller]/updateRent
        [HttpPut]
        [Route("updateprice")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateApartment([FromBody] Model.Apartment apartment)
        {
            var oldapartment = await _context.Apartment.SingleOrDefaultAsync(a => a.Id == apartment.Id);
            if (oldapartment == null)
            {
                return NotFound(new { Message = $"Apartment with id {apartment.Id} not found." });
            }
            var oldPrice = oldapartment.Price;
            bool raiseApartmentPriceChangedEvent = oldPrice != apartment.Price;

            oldapartment = apartment;
            _context.Apartment.Update(oldapartment);
            if (raiseApartmentPriceChangedEvent)
            {
                var priceChangedEvent = new PriceChangedIntegrationEvent(oldapartment.Id, apartment.Price, oldPrice);

                await _apartmentIntegrationEventService.SaveEventAndApartmentContextChangesAsync(priceChangedEvent);

                await _apartmentIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);
            }
            else
            {
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(UpdateApartment), new { id = oldapartment.Id });
        }
        */
    }
}
