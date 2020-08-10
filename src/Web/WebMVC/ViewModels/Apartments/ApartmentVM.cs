using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebMVC.Model;

namespace WebMVC.ViewModels.Apartments
{
    public class Lists
    {
        public IEnumerable<SelectListItem> Owners { get; set; }
        public IEnumerable<SelectListItem> Furnishings { get; set; }
        public IEnumerable<SelectListItem> Bedrooms { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }

    public class RentVM: Lists
    {
        public Rent Rent { get; set; }
        public IEnumerable<SelectListItem> Periods { get; set; }
    }
    public class SaleVM: Lists
    {
        public Sale Sale { get; set; }
    }

    public class ApartmentVM
    {
        public RentVM RentVM { get; set; }
        public SaleVM SaleVM { get; set; }
    }
}
