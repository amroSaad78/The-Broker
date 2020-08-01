using Apartment.API.Infrastructure;
using ApartmentsApi.Proto;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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

        public override async Task<ApartmentResponse> GetApartmentById(ApartmentRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call ApartmentService.GetApartmentById for apartment id {Id}", request.Id);
            if (request.Id <= 0)
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Id must be > 0 (received {request.Id})");
                return null;
            }
            var apartment = await _dbContext.Apartment
                                    .Include("Bedroom")
                                    .Include("Country")
                                    .Include("Furniture")
                                    .Include("Period")
                                    .Include("Purpose")
                                    .SingleOrDefaultAsync(ci => ci.Id == request.Id);
            if (apartment != null)
            {
                apartment.MapToApartmentResponse();
            }
            context.Status = new Status(StatusCode.NotFound, $"Apartment with id {request.Id} do not exist");
            return null;
        }

    }
}
