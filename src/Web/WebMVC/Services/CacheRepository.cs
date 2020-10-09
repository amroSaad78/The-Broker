using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public class CacheRepository : ICacheRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly ILogger<CacheRepository> _logger;
        public CacheRepository(ILogger<CacheRepository> logger, ConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = _redis.GetDatabase();
        }
        public async Task<string> GetListsAsync(string key) => await _database.StringGetAsync(key);
        public async Task DeleteListsAsync(string key)
        {
            if(!await _database.KeyDeleteAsync(key))
                _logger.LogError($"Error: Cache memory was not deleted.");
            
        }
        public async Task SetListsAsync(string key, string lists)
        {
            if(!await _database.StringSetAsync(key, lists))
                _logger.LogError("Error: Cache memory was not updated.");
        }
    }
}
