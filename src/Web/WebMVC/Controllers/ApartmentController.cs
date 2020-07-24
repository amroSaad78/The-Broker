using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMVC.Services;
using WebMVC.ViewModels.Apartments;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ApartmentController: BasicController<ApartmentController> ,IPageController
    {
        private readonly IApartmentService _apartmentService;
        private readonly IOwnersService _ownersService;

        public ApartmentController(IPagesNames pagesNames, IApartmentService apartmentService, IOwnersService ownersService) : base(pagesNames.GetProperty()) 
        {
            _apartmentService = apartmentService;
            _ownersService = ownersService;
        }
        
        [HttpGet]
        public async Task<IActionResult> New()
        {
            var apartment = _apartmentService.GetNewApartment();
            var vm = new ApartmentVM()
            {
                Apartment = apartment,
                Owners = await _ownersService.GetOwners(),
                Bedrooms = await _apartmentService.GetBedrooms(),
                Countries = await _apartmentService.GetCountries(),
                Furnishings = await _apartmentService.GetFurnishings(),
                Periods = await _apartmentService.GetPeriods(),
                Purpose = await _apartmentService.GetPurposes()
            };
            return ActionResult(vm);
        }

        [HttpPost]
        public async Task<IActionResult> New(string anyst)
        {
            return ActionResult();
        }
    }
}