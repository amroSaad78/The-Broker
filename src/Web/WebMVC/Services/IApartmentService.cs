using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Services
{
    public interface IApartmentService
    {
        Task<Apartment> GetApartment(int id);
        Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take);
        Task<IEnumerable<SelectListItem>> GetBedrooms();
        Task<IEnumerable<SelectListItem>> GetCountries();
        Task<IEnumerable<SelectListItem>> GetFurnishings();
        Task<IEnumerable<SelectListItem>> GetPeriods();
        Task<IEnumerable<SelectListItem>> GetPurpose();
    }
}
