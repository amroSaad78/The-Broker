using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OwnersAPI.Proto;
using System.Net.Http;
using System.Threading.Tasks;
using WebClientAgg.Config;

namespace WebClientAgg.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly ILogger<OwnersService> _logger;
        private readonly UrlsConfig _urls;
        private readonly HttpClient _httpClient;

        public OwnersService(HttpClient httpClient, ILogger<OwnersService> logger, IOptions<UrlsConfig> config)
        {
            _logger = logger;
            _urls = config.Value;
            _httpClient = httpClient;
        }
        public async Task<OwnerResponse> GetOwnerById(int id) =>
            await GrpcCallerService.CallService(_urls.GrpcOwners, async channel =>
            {
                var client = new OwnersData.OwnersDataClient(channel);
                _logger.LogDebug("grpc client created, request = {@id}", id);
                var response = await client.GetOwnerByIdAsync(new OwnerRequest { Id = id });
                _logger.LogDebug("grpc response {@response}", response);
                return response;
            });
        public async Task<OwnersResponse> GetOwners() =>
            await GrpcCallerService.CallService(_urls.GrpcOwners, async channel =>
            {
                var client = new OwnersData.OwnersDataClient(channel);
                _logger.LogDebug("grpc client for owners api was created...");
                var response = await client.GetOwnersAsync(new OwnersRequest { });
                _logger.LogDebug("grpc response {@response}", response);
                return response;
            });
        public async Task<OwnersBasicResponse> GetBasicOwners() =>
            await GrpcCallerService.CallService(_urls.GrpcOwners, async channel =>
            {
                var client = new OwnersData.OwnersDataClient(channel);
                _logger.LogDebug("grpc client for owners api was created...");
                var response = await client.GetBasicOwnersAsync(new OwnersRequest { });
                _logger.LogDebug("grpc response {@response}", response);
                return response;
            });
    }
}
