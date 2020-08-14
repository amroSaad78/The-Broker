using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Extension;
using WebMVC.Infrastructure.Filters;
using WebMVC.Model;
using WebMVC.Services;
using WebMVC.ViewModels.Apartments;

namespace WebMVC.Controllers
{
    public class ApartmentController: BasicController<ApartmentController> ,IPageController
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IPagesNames pagesNames, IApartmentService apartmentService) :
            base(pagesNames.GetProperty()) 
        {
            _apartmentService = apartmentService;            
        }

        public IActionResult Messages(Messages msg) => ActionResult(msg);

        [HttpGet]
        public async Task<IActionResult> New(Messages msg = null)
        {
            try
            {                
                var vm = await PopulateVm();
                ViewBag.msg = msg;
                return ActionResult(vm);
            }
            catch
            {
                msg = new Messages
                {
                    Message = "Error: apartment service is inoperative, try again later"
                };
                return RedirectToAction(nameof(Messages), msg);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventDuplicateRequest]
        [ServiceFilter(typeof(CheckUploadedFile<IOptions<AppSettings>>))]
        public async Task<IActionResult> Rent(RentVM rentVM, IFormFile file)
        {
            Messages msg = new Messages();

            if (ModelState.IsValid)
            {
                try
                {
                    await _apartmentService.UploadImage(file);
                    await _apartmentService.SaveRent(rentVM.Rent);
                    msg.Type = ToastTypes.success;
                    msg.Message = "Apartment data was saved.";
                    msg.Title = "Success";
                }
                catch
                {
                    msg.Type = ToastTypes.error;
                    msg.Message = "Apartment service is inoperative, try again later.";
                    msg.Title = "Error";
                }
            }
            return RedirectToAction(nameof(New), msg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventDuplicateRequest]
        public async Task<IActionResult> Sale(SaleVM saleVM, IFormFile file)
        {
            Messages msg = new Messages();
            if (ModelState.IsValid)
            {
                try
                {
                    await _apartmentService.SaveSale(saleVM.Sale);
                    msg.Type = ToastTypes.success;
                    msg.Message = "Apartment data was saved.";
                    msg.Title = "Success";
                }
                catch
                {
                    msg.Type = ToastTypes.error;
                    msg.Message = "Apartment service is inoperative, try again later.";
                    msg.Title = "Error";
                }
            }
            return RedirectToAction(nameof(New), msg);
        }

        private async Task<ApartmentVM> PopulateVm()
        {
            var lists = await _apartmentService.PopulateLists();
            var rent = new RentVM()
            {
                Bedrooms = lists.GetSelectListAsync("bedrooms", "bedroomsCount"),
                Countries = lists.GetSelectListAsync("countries", "country").OrderBy(o => o.Text),
                Furnishings = lists.GetSelectListAsync("furnishings", "furnitureType"),
                Periods = lists.GetSelectListAsync("periods", "period"),
                Owners = lists.GetSelectListAsync("owners", "fullName").OrderBy(o => o.Text)                
            };
            var sale = new SaleVM()
            {
                Bedrooms = rent.Bedrooms,
                Countries = rent.Countries,
                Furnishings = rent.Furnishings,
                Owners = rent.Owners
            };
            return new ApartmentVM { RentVM= rent, SaleVM= sale };
        }
    }
}