using ApartmentsApi.Proto;
using Google.Protobuf.Collections;
using OwnersAPI.Proto;
using System.Collections.Generic;
using System.Linq;
using WebClientAgg.Model;

namespace WebClientAgg.Extensions
{
    public static class GrpcExtensions
    {
        public static Apartment MapToApartment(this ApartmentResponse apartment)
        {
            if (apartment == null) return null;

            var map = new Apartment
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
                Price = (decimal)apartment.Price,
                Installment = apartment.Installment,
                OwnerId = apartment.OwnerId,
                BedroomId = apartment.BedroomId,
                CountryId = apartment.CountryId,
                FurnitureId = apartment.FurnitureId,
                PeriodId = apartment.PeriodId,
                Purpose = apartment.Purpose,
                Bedroom = new Bedrooms { Id = apartment.Bedroom.Id, BedroomsCount = apartment.Bedroom.BedroomsCount },
                Country = new Countries { Id = apartment.Country.Id, Country = apartment.Country.Country },
                Furniture = new Furnishings { Id = apartment.Furniture.Id, FurnitureType = apartment.Furniture.FurnitureType },
                Period = new Periods { Id = apartment.Period.Id, Period = apartment.Period.Period }
            };
            return map;
        }

        public static List<Bedrooms> MapToBedrooms(this RepeatedField<GrpcBedrooms> bedrooms)
        {
            var BedroomsResponse = new List<Bedrooms>();
            bedrooms.ToList().ForEach(i =>
            {
                BedroomsResponse.Add(new Bedrooms { Id = i.Id, BedroomsCount = i.BedroomsCount });
            });
            return BedroomsResponse;
        }
        public static List<Countries> MapToCountries(this RepeatedField<GrpcCountries> countries)
        {
            var CountriesResponse = new List<Countries>();
            countries.ToList().ForEach(i =>
            {
                CountriesResponse.Add(new Countries { Id = i.Id, Country = i.Country });
            });
            return CountriesResponse;
        }
        public static List<Furnishings> MapToFurnishings(this RepeatedField<GrpcFurnishings> furnishings)
        {
            var FurnishingsResponse = new List<Furnishings>();
            furnishings.ToList().ForEach(i =>
            {
                FurnishingsResponse.Add(new Furnishings { Id = i.Id, FurnitureType = i.FurnitureType });
            });
            return FurnishingsResponse;
        }
        public static List<Periods> MapToPeriods(this RepeatedField<GrpcPeriods> periods)
        {
            var PeriodsResponse = new List<Periods>();
            periods.ToList().ForEach(i =>
            {
                PeriodsResponse.Add(new Periods { Id = i.Id,  Period = i.Period });
            });
            return PeriodsResponse;
        }
        public static Owner MapToOwner(this OwnerResponse grpcOwner) => new Owner
        {
            Id = grpcOwner.Id,
            FirstName = grpcOwner.FirstName,
            LastName = grpcOwner.LastName,
            Email = grpcOwner.Email,
            Company = grpcOwner.Company,
            Address = grpcOwner.Address,
            City = grpcOwner.City,
            Mobile = grpcOwner.Mobile,
            ZIP = grpcOwner.ZIP
        };

    }
}
