using OwnersAPI.Proto;
using System.Threading.Tasks;

namespace WebClientAgg.Services
{
    public interface IOwnersService
    {
        Task<OwnersResponse> GetOwners();
        Task<OwnersBasicResponse> GetBasicOwners();
        Task<OwnerResponse> GetOwnerById(int id);
    }
}
