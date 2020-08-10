using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Services
{
    public interface IApartmentService
    {
        Task<Apartment> GetApartment(int id);
        Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take);
        Task<string> PopulateLists();
        Task SaveRent(Rent apartment);
        Task SaveSale(Sale apartment);
    }
}
