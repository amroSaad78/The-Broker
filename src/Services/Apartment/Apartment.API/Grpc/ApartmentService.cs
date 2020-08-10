using Apartment.API.Infrastructure;
using ApartmentsApi.Proto;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ApartmentsApi.Proto.ApartmentData;

namespace Apartment.API.Grpc
{
    public class ApartmentService: ApartmentDataBase
    {
        private readonly ApartmentContext _dbContext;
        private readonly ApartmentSettings _settings;
        private readonly ILogger<ApartmentService> _logger;

        public ApartmentService(ApartmentContext dbContext, IOptions<ApartmentSettings> settings, ILogger<ApartmentService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
            _settings = settings.Value;
            _logger = logger;
        }

        public override async Task<ListsResponse> PopulateLists(ListsRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call ApartmentService.PopulateLists.");
            var bedrooms = await _dbContext.Bedroom?.Select(b => b.MapToGrpcBedrooms()).ToListAsync();
            var countries = await _dbContext.Country?.Select(c => c.MapToGrpcCountries()).ToListAsync();
            var furnishings = await _dbContext.Furniture?.Select(f => f.MapToGrpcFurnishings()).ToListAsync();
            var periods = await _dbContext.Period?.Select(p => p.MapToGrpcPeriods()).ToListAsync();

            var listResponse = new ListsResponse();
            listResponse.Grpcbedrooms.AddRange(bedrooms);
            listResponse.Grpccountries.AddRange(countries);
            listResponse.Grpcfurnishings.AddRange(furnishings);
            listResponse.Grpcperiods.AddRange(periods);

            return listResponse;
        }

    }
}
