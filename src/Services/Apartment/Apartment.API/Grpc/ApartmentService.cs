using Apartment.API.Infrastructure;
using Apartment.API.Proto;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static Apartment.API.Proto.Apartment;

namespace Apartment.API.Grpc
{
    public class ApartmentService: ApartmentBase
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
            _logger.LogInformation("Begin grpc call ApartmentService.GetById for apartment id {Id}", request.Id);
            if (request.Id <= 0)
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Id must be > 0 (received {request.Id})");
                return null;
            }
            var apartment = await _dbContext.Apartment.SingleOrDefaultAsync(ci => ci.Id == request.Id);

            if (apartment != null)
            {
                return new ApartmentResponse()
                {
                    Id = apartment.Id,
                    Parking = apartment.Parking,
                    Reception = apartment.Reception,
                    Kitchens = apartment.Kitchens,
                    Bathrooms = apartment.Bathrooms,
                    Area = apartment.Area,
                    View = apartment.View,
                    Floor = apartment.Floor,
                    Flat = apartment.Flat,
                    City = apartment.City,
                    Region = apartment.Region,
                    Adresse = apartment.Adresse,
                    Price = (double) apartment.Price,
                    Installment = apartment.Installment,
                    OwnerId = apartment.OwnerId,
                    BedroomId = apartment.BedroomId,
                    CountryId = apartment.CountryId,
                    FurnitureId = apartment.FurnitureId,
                    PeriodId = apartment.PeriodId,
                    PurposeId = apartment.PurposeId
                };
            }

            context.Status = new Status(StatusCode.NotFound, $"Apartment with id {request.Id} do not exist");
            return null;
        }
    }
}
