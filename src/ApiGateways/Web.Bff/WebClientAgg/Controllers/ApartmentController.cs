using ApartmentsApi.Proto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwnersAPI.Proto;
using System.Net;
using System.Threading.Tasks;
using WebClientAgg.Extensions;
using WebClientAgg.Model;
using WebClientAgg.Model.DTO;
using WebClientAgg.Services;

namespace WebClientAgg.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IOwnersService _ownersService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentService apartmentService, IOwnersService ownersService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _ownersService = ownersService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApartmentSubLists), (int)HttpStatusCode.OK)]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ApartmentSubLists>> PopulateLists()
        {
            var owners = await _ownersService.GetBasicOwners();
            var lists = await _apartmentService.PopulateLists();
            return Ok(new ApartmentSubLists()
            {
                Bedrooms = lists?.Grpcbedrooms?.MapToModel((GrpcBedrooms i) =>  _mapper.Map<Bedrooms>(i)),
                Countries = lists?.Grpccountries?.MapToModel((GrpcCountries c) =>  _mapper.Map<Countries>(c)),
                Furnishings = lists?.Grpcfurnishings?.MapToModel((GrpcFurnishings f) => _mapper.Map<Furnishings>(f)),
                Owners = owners?.GrpcOwnersBasic?.MapToModel((OwnerBasicResponse o) =>  _mapper.Map<BasicOwner>(o)),
                Periods = lists?.Grpcperiods?.MapToModel((GrpcPeriods p) => _mapper.Map<Periods>(p)),
            });
        }
    }
}
