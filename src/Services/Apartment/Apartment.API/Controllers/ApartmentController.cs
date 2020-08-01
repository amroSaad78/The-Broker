using Apartment.API.Infrastructure;
using Apartment.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ApartmentSettings _settings;

        public ApartmentController(ApartmentContext context, IOptionsSnapshot<ApartmentSettings> settings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _settings = settings.Value;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        
        // GET api/v1/[controller]/bedrooms
        [HttpGet]
        [Route("bedrooms")]
        [ProducesResponseType(typeof(Bedrooms), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Bedrooms>>> GetBedrooms()
        {
            var _bedrooms = await _context.Bedroom.OrderBy(i => i.Id).ToListAsync();
            return Ok(_bedrooms);
        }

        // GET api/v1/[controller]/countries
        [HttpGet]
        [Route("countries")]
        [ProducesResponseType(typeof(Countries), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Countries>>> GetCountries()
        {
            var _countries = await _context.Country.OrderBy(i => i.Country).ToListAsync();
            return Ok(_countries);
        }

        // GET api/v1/[controller]/furnishings
        [HttpGet]
        [Route("furnishings")]
        [ProducesResponseType(typeof(Furnishings), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Furnishings>>> GetFurnishings()
        {
            var _furniture = await _context.Furniture.OrderBy(i => i.Id).ToListAsync();
            return Ok(_furniture);
        }

        // GET api/v1/[controller]/periods
        [HttpGet]
        [Route("periods")]
        [ProducesResponseType(typeof(Periods), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Periods>>> GetPeriods()
        {
            var _periods = await _context.Period.OrderBy(i => i.Id).ToListAsync();
            return Ok(_periods);
        }

        // GET api/v1/[controller]/purpose
        [HttpGet]
        [Route("purpose")]
        [ProducesResponseType(typeof(Purpose), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Purpose>>> GetPurpose()
        {
            var _purpose = await _context.Purpose.OrderBy(i => i.Id).ToListAsync();
            return Ok(_purpose);
        }

    }
}
