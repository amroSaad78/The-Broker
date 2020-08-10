using ApartmentsApi.Proto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using WebClientAgg.Config;

namespace WebClientAgg.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly ILogger<ApartmentService> _logger;
        private readonly UrlsConfig _urls;
        private readonly HttpClient _httpClient;
        public ApartmentService(HttpClient httpClient, ILogger<ApartmentService> logger, IOptions<UrlsConfig> config)
        {
            _logger = logger;
            _urls = config.Value;
            _httpClient = httpClient;
        }
        public async Task<ListsResponse> PopulateLists() =>
            // using http2 or grpc for communcation between aggregators and services like apartment or owners service.
            await GrpcCallerService.CallService(_urls.GrpcApartment, async channel =>
            {
                var client = new ApartmentData.ApartmentDataClient(channel);
                _logger.LogDebug("grpc client for apartment api was created...");
                var response = await client.PopulateListsAsync(new ListsRequest { });
                _logger.LogDebug("grpc response {@response}", response);
                return response;
            });
    }
}
