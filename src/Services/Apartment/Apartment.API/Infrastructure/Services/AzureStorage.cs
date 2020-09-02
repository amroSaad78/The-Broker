using Apartment.API.Model;
using MediatR;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Services
{
    public class AzureStorage : IStorage
    {
        public Task SaveAsync(FileData fileData)
        {
            return null;
        }
    }
}
