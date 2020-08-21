using System.ComponentModel.DataAnnotations;
using WebMVC.Services.Signatures;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace WebMVC.Model
{
    public class Apartment
    {
        public int Id { get; set; }
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
        public bool Parking { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required(ErrorMessage = "Bedrooms no. is required.")]
        public int BedroomId { get; set; }
        public Bedrooms Bedroom { get; set; }
        [Required(ErrorMessage = "Country name is required.")]
        public int CountryId { get; set; }
        public Countries Country { get; set; }
        [Required(ErrorMessage = "Furniture type is required.")]
        public int FurnitureId { get; set; }
        public Furnishings Furniture { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public bool BookedUp { get; set; }
    }

    public class Rent : IPayload
    {
        [Required(ErrorMessage = "Period is required.")]
        public int PeriodId { get; set; }
        public Periods Period { get; set; }
    }

    public class Sale : IPayload
    {
        public bool Installment { get; set; }
    }


    public class Payload<Tin> where Tin : IPayload
    {
        public Payload(Apartment apartment , Tin inobj)
        {
            Apartment = apartment;
            InObject = inobj;
        }

        public Apartment Apartment { get; set; }
        public Tin InObject { get; set; }
    }
}
