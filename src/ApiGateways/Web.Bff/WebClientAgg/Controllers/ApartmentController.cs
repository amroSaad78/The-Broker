using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClientAgg.Services;

namespace WebClientAgg.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IOwnersService _ownersService;

        public ApartmentController(IApartmentService apartmentService, IOwnersService ownersService)
        {
            _apartmentService = apartmentService;
            _ownersService = ownersService;
        }

        
    }
}
