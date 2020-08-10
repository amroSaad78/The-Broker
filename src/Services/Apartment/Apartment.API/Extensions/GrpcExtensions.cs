using Apartment.API.Model;
using ApartmentsApi.Proto;

namespace Apartment.API.Grpc
{
    public static class GrpcExtensions
    {
        public static GrpcBedrooms MapToGrpcBedrooms(this Bedrooms bedrooms) => new GrpcBedrooms()
        {
            Id = bedrooms.Id,
            BedroomsCount = bedrooms.BedroomsCount
        };
        public static GrpcCountries MapToGrpcCountries(this Countries countries) => new GrpcCountries()
        {
            Id = countries.Id,
            Country = countries.Country
        };
        public static GrpcFurnishings MapToGrpcFurnishings(this Furnishings furnishings) => new GrpcFurnishings()
        {
            Id = furnishings.Id,
            FurnitureType = furnishings.FurnitureType
        };
        public static GrpcPeriods MapToGrpcPeriods(this Periods periods) => new GrpcPeriods()
        {
            Id = periods.Id,
            Period = periods.Period
        };

    }
}
