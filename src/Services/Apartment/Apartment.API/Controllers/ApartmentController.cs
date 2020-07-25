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

        // GET api/v1/[controller]/GetBedrooms
        [HttpGet]
        [Route("bedrooms")]
        [ProducesResponseType(typeof(List<Bedrooms>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Bedrooms>>> GetBedrooms() => Ok(await _context.Bedroom.OrderBy(i => i.Id).ToListAsync());

        // GET api/v1/[controller]/GetCountries
        [HttpGet]
        [Route("countries")]
        [ProducesResponseType(typeof(List<Countries>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Countries>>> GetCountries() => Ok(await _context.Country.OrderBy(i => i.Country).ToListAsync());

        // GET api/v1/[controller]/GetFurnishings
        [HttpGet]
        [Route("furnishings")]
        [ProducesResponseType(typeof(List<Furnishings>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Furnishings>>> GetFurnishings() => Ok(await _context.Furniture.OrderBy(i => i.Id).ToListAsync());

        // GET api/v1/[controller]/GetPeriods
        [HttpGet]
        [Route("periods")]
        [ProducesResponseType(typeof(List<Periods>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Periods>>> GetPeriods() => Ok(await _context.Period.OrderBy(i => i.Id).ToListAsync());

        // GET api/v1/[controller]/GetPurpose
        [HttpGet]
        [Route("purpose")]
        [ProducesResponseType(typeof(List<Purpose>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Purpose>>> GetPurpose() => Ok(await _context.Purpose.ToListAsync());

    }
}
