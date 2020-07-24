using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Model
{
    public class Apartment
    {
        public int Id { get; set; }
        public bool Parking { get; set; }        
        public int Reception { get; set; }
        public int Kitchens { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; }
        public string View { get; set; }
        public int Floor { get; set; }
        public int Flat { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Adresse { get; set; }
        public int Price { get; set; }
        public bool Installment { get; set; }
        public int OwnerId { get; set; }
        public int BedroomId { get; set; }
        public int CountryId { get; set; }
        public int FurnitureId { get; set; }
        public int PeriodId { get; set; }
        public int PurposeId { get; set; }
    }
}
