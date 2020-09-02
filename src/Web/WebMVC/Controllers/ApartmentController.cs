using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    //Todo: adding cashing service for lists and adding signalR client
    //using https and win sql server
    public class ApartmentController: BasicController<ApartmentController> ,IPageController
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IPagesNames pagesNames, IApartmentService apartmentService) :
            base(pagesNames.GetProperty()) 
        {
            _apartmentService = apartmentService;            
        }

        public IActionResult Blank()
        {
            var msg = new Messages();
            msg.SetMessage(ToastTypes.error,"Apartment service is inoperative.");
            ViewBag.msg = msg;
            return ActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            try
            {
                string result = await _apartmentService.PopulateLists();
                var model = PopulateLists(result,(_, __, ___) => { });
                return ActionResult(model);
            }
            catch
            {
                return RedirectToAction(nameof(Blank));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PreventDuplicateRequest]
        public async Task<IActionResult> New(GenricApartment<ListsVM, RentVM, SaleVM, ApartmentVM> apartment, IFormFile file)
        {
            Messages msg = new Messages();
            if (!ModelState.IsValid)
            {
                msg.SetMessage(ToastTypes.error, "There is an error in the entered data.", "Error");
            }
            else
            {
                try
                {
                    IPayload entity = (apartment.In2, apartment.In3) switch
                    {
                        (null, _) => apartment.In3.Sale,
                        (_, null) => apartment.In2.Rent,
                        _ => throw new ArgumentOutOfRangeException("Invalid arguments values.")
                    };
                    Guid requestId = Guid.NewGuid();
                    var payload = new Payload<IPayload>(apartment.In4.Apartment, entity);
                    payload.Apartment.RequestId = requestId;
                    await _apartmentService.Save(payload);
                    ModelState.Clear();
                    await _apartmentService.UploadImage(file, requestId);
                    msg.SetMessage(ToastTypes.success, "Apartment data was saved.", "Success");
                }
                catch (Exception ex)
                {
                    if(ex.GetType() == typeof(ArgumentException))
                        msg.SetMessage(ToastTypes.warning, ex.Message ?? "Apartment service is inoperative.", "Warning");
                    else
                        msg.SetMessage(ToastTypes.error, ex.Message ?? "Apartment service is inoperative.");
                }
            }
            try
            {
                ViewBag.msg = msg;
                string result = await _apartmentService.PopulateLists();
                var model = PopulateLists(result,(Rent rent, Sale sale, Apartment apartData) =>
                    {
                        rent = apartment.In2?.Rent;
                        sale = apartment.In3?.Sale;
                        apartData = apartment.In4?.Apartment;
                    });
                return ActionResult(model);
            }
            catch
            {
                return RedirectToAction(nameof(Blank));
            }
        }

        private GenricApartment<ListsVM, RentVM, SaleVM, ApartmentVM> PopulateLists(string lists, Action<Rent, Sale, Apartment> action)
        {
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