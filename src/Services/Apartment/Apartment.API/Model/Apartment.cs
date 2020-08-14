using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Bedrooms Bedroom { get; set; }
        public int CountryId { get; set; }
        public Countries Country { get; set; }
        public int FurnitureId { get; set; }
        public Furnishings Furniture { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public bool BookedUp { get; set; }
    }

    public class Rent: Apartment
    {
        public int PeriodId { get; set; }
        public Periods Period { get; set; }
    }

    public class Sale: Apartment
    {
        public bool Installment { get; set; }
    }
}
