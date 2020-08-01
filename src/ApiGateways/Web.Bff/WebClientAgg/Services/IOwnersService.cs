using System.Collections.Generic;
using System.Threading.Tasks;
using WebClientAgg.Model;
using WebClientAgg.Model.DTO;

namespace WebClientAgg.Services
{
    public interface IOwnersService
    {
        Task<List<Owner>> GetOwners();
        Task<Owner> GetOwnerById(int id);
    }
}
