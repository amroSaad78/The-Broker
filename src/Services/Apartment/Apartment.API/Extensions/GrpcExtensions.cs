using Apartment.API.Model;
using ApartmentsApi.Proto;
using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace Apartment.API.Grpc
{
    public static class GrpcExtensions
    {
        public static ApartmentResponse MapToApartmentResponse(this Apartments apartment) => new ApartmentResponse()
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
            Price = (double)apartment.Price,
            Installment = apartment.Installment,
            OwnerId = apartment.OwnerId,
            BedroomId = apartment.BedroomId,
            CountryId = apartment.CountryId,
            FurnitureId = apartment.FurnitureId,
            PeriodId = apartment.PeriodId,
            PurposeId = apartment.PurposeId,
            Bedroom = new GrpcBedrooms { Id = apartment.Bedroom.Id, BedroomsCount = apartment.Bedroom.BedroomsCount },
            Country = new GrpcCountries { Id = apartment.Country.Id, Country = apartment.Country.Country },
            Furniture = new GrpcFurnishings { Id = apartment.Furniture.Id, FurnitureType = apartment.Furniture.FurnitureType },
            Period = new GrpcPeriods { Id = apartment.Period.Id, Period = apartment.Period.Period },
            Purpose = new GrpcPurpose { Id = apartment.Purpose.Id, PurposeType = apartment.Purpose.PurposeType }
        };
    }
}
