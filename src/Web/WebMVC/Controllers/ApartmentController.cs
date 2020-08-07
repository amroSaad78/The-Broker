using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMVC.Model;
using WebMVC.Services;
using WebMVC.ViewModels.Apartments;

namespace WebMVC.Controllers
{
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

        public IActionResult Messages(Messages msg)
        {
            return ActionResult(msg);
        }

        [HttpGet]
        public async Task<IActionResult> New(Messages msg = null)
        {
            try
            {
                var vm = await PopulateVm();
                vm.Apartment = new Apartment();
                ViewBag.msg = msg;
                return ActionResult(vm);
            }
            catch
            {                
                msg = new Messages { Message = $"Error: apartment service is inoperative, try again later." };
                return RedirectToAction(nameof(Messages), msg);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ApartmentVM apartmentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _apartmentService.SaveApartment(apartmentVM.Apartment);
                    var msg = new Messages { Types=ToastTypes.success, Message="Apartment data was saved.", Title="Success" };
                    return RedirectToAction(nameof(New), msg);
                }
                catch
                {
                    ViewBag.msg = new Messages { Types = ToastTypes.error, Message = "Apartment service is inoperative, try again later.", Title = "Error" };
                }
            }
            var vm = await PopulateVm();
            return ActionResult(vm);
        }

        private async Task<ApartmentVM> PopulateVm()
        {
            var bedrooms = await _apartmentService.GetBedrooms();
            var countries = await _apartmentService.GetCountries();
            var furnishings = await _apartmentService.GetFurnishings();
            var periods = await _apartmentService.GetPeriods();
            var owners = await _ownersService.GetBasicOwners();

            return new ApartmentVM()
            {
                Bedrooms = bedrooms,
                Countries = countries,
                Furnishings = furnishings,
                Periods = periods,
                Owners = owners
            };
        }
    }
}