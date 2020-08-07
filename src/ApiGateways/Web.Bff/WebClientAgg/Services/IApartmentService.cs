using System.Threading.Tasks;
using WebClientAgg.Model;
using WebClientAgg.Model.DTO;

namespace WebClientAgg.Services
{
    public interface IApartmentService
    {
        Task<Apartment> GetApartmentById(int id);
    }
}
