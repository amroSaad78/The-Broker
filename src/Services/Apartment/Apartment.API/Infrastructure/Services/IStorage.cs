using Apartment.API.Model;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Services
{
    public interface IStorage
    {
        Task SaveAsync(FileData fileData);
    }
}
