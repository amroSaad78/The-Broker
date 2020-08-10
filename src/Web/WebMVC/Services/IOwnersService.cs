using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Services
{
    public interface IOwnersService
    {
        Task<IEnumerable<Owner>> GetOwners();
    }
}
