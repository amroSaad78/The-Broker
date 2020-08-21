using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebMVC.Model;

namespace WebMVC.ViewModels
{
    public class ListsVM
    {
        public IEnumerable<SelectListItem> Owners { get; set; }
        public IEnumerable<SelectListItem> Furnishings { get; set; }
        public IEnumerable<SelectListItem> Bedrooms { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }

    public class RentVM
    {
        public Rent Rent { get; set; }
        public IEnumerable<SelectListItem> Periods { get; set; }
    }
    public class SaleVM
    {
        public Sale Sale { get; set; }
    }

    public class ApartmentVM
    {
        public Apartment Apartment { get; set; }
    }
}
