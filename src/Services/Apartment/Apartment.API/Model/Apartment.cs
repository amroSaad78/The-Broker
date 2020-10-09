using Apartment.API.Infrastructure.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Apartment.API.Model
{
    public class Apartment
    {
        public int Id { get; set; }
        public bool Parking { get; set; }
        public int Reception { get; set; }
        public int Kitchens { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; }
        [MaxLength(50)]
        public string View { get; set; }
        public int Floor { get; set; }
        public int Flat { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(100)]
        public string Region { get; set; }
        [Required, MaxLength(100)]
        public string Adresse { get; set; }
        public decimal Price { get; set; }
        public int OwnerId { get; set; }
        public int BedroomId { get; set; }
        [JsonIgnore]
        public Bedrooms Bedroom { get; set; }
        public int CountryId { get; set; }
        [JsonIgnore]
        public Countries Country { get; set; }
        public int FurnitureId { get; set; }
        [JsonIgnore]
        public Furnishings Furniture { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public bool BookedUp { get; set; }
        [JsonIgnore, Required]
        public Guid RequestId { get; set; }
    }

    public class Rent: IPayload
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        [JsonIgnore]
        public Apartment Apartment { get; set; }
        public int PeriodId { get; set; }
        [JsonIgnore]
        public Periods Period { get; set; }
    }

    public class Sale: IPayload
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        [JsonIgnore]
        public Apartment Apartment { get; set; }
        public bool Installment { get; set; }
    }

    public class Payload<Tin> where Tin : IPayload
    {
        public Payload(Apartment apartment, Tin inobj)
        {
            Apartment = apartment;
            InObject = inobj;
        }

        public Apartment Apartment { get; set; }
        public Tin InObject { get; set; }
    }
}
