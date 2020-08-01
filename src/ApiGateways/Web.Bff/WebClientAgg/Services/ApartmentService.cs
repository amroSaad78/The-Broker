using ApartmentsApi.Proto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using WebClientAgg.Config;
using WebClientAgg.Extensions;
using WebClientAgg.Model;

namespace WebClientAgg.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApartmentService> _logger;
        private readonly UrlsConfig _urls;
        public ApartmentService(HttpClient httpClient, ILogger<ApartmentService> logger, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }
        public async Task<Apartments> GetApartmentById(int id) =>
            // using http2 or grpc for communcation between aggregators and services like apartment or owners service.
            await GrpcCallerService.CallService(_urls.GrpcApartment, async channel =>
            {
                var client = new ApartmentData.ApartmentDataClient(channel);
                _logger.LogDebug("grpc client created, request = {@id}", id);
                var response = await client.GetApartmentByIdAsync(new ApartmentRequest { Id = id });
                _logger.LogDebug("grpc response {@response}", response);
                return response.MapToApartment();
            });
    }
}
