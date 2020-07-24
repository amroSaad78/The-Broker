using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public static class API
    {
        public static class Apartment
        {
            public static string Bedrooms(string baseUri) => $"{baseUri}/bedrooms";
            public static string Countries(string baseUri) => $"{baseUri}/countries";
            public static string Furnishings(string baseUri) => $"{baseUri}/furnishings";
            public static string Owners(string baseUri) => $"{baseUri}/owners";
            public static string Periods(string baseUri) => $"{baseUri}/periods";
            public static string Purposes(string baseUri) => $"{baseUri}/purposes";
        }
    }
}
