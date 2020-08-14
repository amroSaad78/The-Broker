using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Owners.API;
using Owners.API.Infrastructure;
using Owners.API.Model;
using Owners.API.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Apartment.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerContext _context;
        private readonly OwnerSettings _settings;

        public OwnerController(OwnerContext context, IOptionsSnapshot<OwnerSettings> settings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _settings = settings.Value;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET api/v1/[controller]/owners
        [HttpGet]
        [Route("owners")]
        [ProducesResponseType(typeof(List<Owner>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Owner>>> GetOwners() => Ok(await _context.Owners.OrderBy(i => i.FirstName).ToListAsync());

        // GET api/v1/[controller]/basicowners
        [HttpGet]
        [Route("basicowners")]
        [ProducesResponseType(typeof(List<BasicOwner>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BasicOwner>>> GetBasicOwners() =>
            Ok(await _context.Owners.Select(o => new BasicOwner() { Id = o.Id, FullName = $"{o.FirstName}  {o.LastName}" }).ToListAsync());
    }
}
