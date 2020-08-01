using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebMVC.Model;
using WebMVC.Services;
using WebMVC.ViewModels.Apartments;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ApartmentController: BasicController<ApartmentController> ,IPageController
    {
        private readonly IApartmentService _apartmentService;
        private readonly IOwnersService _ownersService;

        public ApartmentController(IPagesNames pagesNames, IApartmentService apartmentService, IOwnersService ownersService) :
            base(pagesNames.GetProperty()) 
        {
            _apartmentService = apartmentService;
            _ownersService = ownersService;
        }
        
        [HttpGet]
        public async Task<IActionResult> New()
        {
            try
            {
                var vm = new ApartmentVM()
                {
                    Apartment = new Apartment(),
                    Bedrooms = await _apartmentService.GetBedrooms(),
                    Countries = await _apartmentService.GetCountries(),
                    Furnishings = await _apartmentService.GetFurnishings(),
                    Periods = await _apartmentService.GetPeriods(),
                    Purpose = await _apartmentService.GetPurpose(),
                    Owners = await _ownersService.GetBasicOwners()
                };
                return ActionResult(vm);
            }
            catch(Exception)
            {
                return RedirectToAction(nameof(DashboardController.Login), "Dashboard");
            }
        }

    }
}