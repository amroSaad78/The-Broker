using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebMVC.Model;
using WebMVC.Services.Signatures;

namespace WebMVC.Services
{
    public interface IApartmentService
    {
        Task<Apartment> GetApartment(int id);
        Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take);
        Task<string> PopulateLists();
        Task Save(Payload<IPayload> payload);
        Task UploadImage(IFormFile file);
    }
}
