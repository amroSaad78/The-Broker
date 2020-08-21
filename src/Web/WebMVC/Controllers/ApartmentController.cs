using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Extension;
using WebMVC.Infrastructure.Filters;
using WebMVC.Model;
using WebMVC.Services;
using WebMVC.Services.Signatures;
using WebMVC.ViewModels;

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

        public IActionResult Blank(Messages msg)
        {
            ViewBag.msg = msg;
            return ActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            try
            {
                var vmModel = await PopulateLists((_, __, ___) => { });
                return ActionResult(vmModel);
            }
            catch(Exception ex)
            {
                var msg = new Messages();
                msg.SetMessage(ToastTypes.error, ex.Message);
                return RedirectToAction(nameof(Blank), msg);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventDuplicateRequest]
        //[ServiceFilter(typeof(CheckUploadedFile<IOptions<AppSettings>>))]
        public async Task<IActionResult> New(GenricApartment<ListsVM, RentVM, SaleVM, ApartmentVM> apartment, IFormFile file)
        {
            Messages msg = new Messages();            
            if (ModelState.IsValid)
            {
                try
                {
                    IPayload entity = (apartment.In2, apartment.In3) switch
                    {
                        (null,  _) => apartment.In3.Sale,
                        (_, null) => apartment.In2.Rent,
                        _ => throw new ArgumentOutOfRangeException("Invalid arguments values.")
                    };

                    var payload = new Payload<IPayload>(apartment.In4.Apartment, entity);
                    await _apartmentService.Save(payload); //get id and pass it to sec request
                    await _apartmentService.UploadImage(file);
                    msg.SetMessage(ToastTypes.success, "Apartment data was saved.", "Success");
                    ViewBag.msg = msg;
                    var model = await PopulateLists((_, __, ___) => { });
                    return ActionResult(model);
                }
                catch (Exception ex)
                {
                    msg.SetMessage(ToastTypes.error, ex.Message);
                }
            }
            ViewBag.msg = msg;
            var vmModel = await PopulateLists((Rent rent, Sale sale, Apartment apartData) => {
                rent = apartment.In2?.Rent;
                sale = apartment.In3?.Sale;
                apartData = apartment.In4?.Apartment;
            });
            return ActionResult(vmModel);
        }

        private async Task<GenricApartment<ListsVM, RentVM, SaleVM, ApartmentVM>> PopulateLists(Action<Rent, Sale, Apartment> action)
        {
            var lists = await _apartmentService.PopulateLists();
            ListsVM listsVm = new ListsVM()
            {
                Bedrooms = lists.GetSelectListAsync("bedrooms", "bedroomsCount"),
                Countries = lists.GetSelectListAsync("countries", "country").OrderBy(o => o.Text),
                Furnishings = lists.GetSelectListAsync("furnishings", "furnitureType"),
                Owners = lists.GetSelectListAsync("owners", "fullName").OrderBy(o => o.Text)                
            };
            RentVM rent = new RentVM() { Periods = lists.GetSelectListAsync("periods", "period") };
            SaleVM sale = new SaleVM();
            ApartmentVM apartment = new ApartmentVM();
            action.Invoke(rent.Rent, sale.Sale, apartment.Apartment);
            return new GenricApartment<ListsVM, RentVM, SaleVM, ApartmentVM>(listsVm, rent, sale, apartment);
        }
    }
}