using ApartmentsApi.Proto;
using System.Threading.Tasks;

namespace WebClientAgg.Services
{
    public interface IApartmentService
    {
        Task<ListsResponse> PopulateLists();
    }
}
