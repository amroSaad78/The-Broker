using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebMVC.Model;
using WebMVC.Services;

namespace WebMVC.ViewModels.Apartments
{
    public class ApartmentVM
    {
        public Apartment Apartment { get; set; }
        public IEnumerable<SelectListItem> Owners { get; set; }
        public IEnumerable<SelectListItem> Purpose { get; set; }
        public IEnumerable<SelectListItem> Furnishings { get; set; }        
        public IEnumerable<SelectListItem> Bedrooms { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Periods { get; set; }
    }
}
