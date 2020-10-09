using System.Threading.Tasks;

namespace WebMVC.Services
{
    public interface ICacheRepository
    {
        Task<string> GetListsAsync(string key);
        Task SetListsAsync(string key, string lists);
        Task DeleteListsAsync(string key);
    }
}
