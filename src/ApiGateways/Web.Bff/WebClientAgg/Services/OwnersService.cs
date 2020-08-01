using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OwnersAPI.Proto;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClientAgg.Config;
using WebClientAgg.Extensions;
using WebClientAgg.Model;

namespace WebClientAgg.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OwnersService> _logger;
        private readonly UrlsConfig _urls;

        public OwnersService(HttpClient httpClient, ILogger<OwnersService> logger, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }
        public async Task<Owner> GetOwnerById(int id) =>
            await GrpcCallerService.CallService(_urls.GrpcOwners, async channel =>
            {
                var client = new OwnersData.OwnersDataClient(channel);
                _logger.LogDebug("grpc client created, request = {@id}", id);
                var response = await client.GetOwnerByIdAsync(new OwnerRequest { Id = id });
                _logger.LogDebug("grpc response {@response}", response);
                return response.MapToOwner();
            });
        public async Task<List<Owner>> GetOwners() =>
            await GrpcCallerService.CallService(_urls.GrpcOwners, async channel =>
            {
                var client = new OwnersData.OwnersDataClient(channel);
                _logger.LogDebug("grpc client created...");
                var response = await client.GetOwnersAsync(new OwnersRequest { });
                _logger.LogDebug("grpc response {@response}", response);
                var owners = new List<Owner>();
                response.GrpcOwners.ToList().ForEach(o =>
                {
                    owners.Add(new Owner { 
                        Id = o.Id, 
                        FirstName = o.FirstName, 
                        LastName = o.LastName, 
                        Address = o.Address, 
                        City = o.City, 
                        Company = o.Company,
                        Email = o.Email,
                        Mobile = o.Mobile,
                        ZIP = o.ZIP
                    });
                });

                return owners;
            });
    }
}
