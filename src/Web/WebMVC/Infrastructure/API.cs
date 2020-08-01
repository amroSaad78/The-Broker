namespace WebMVC.Infrastructure
{
    public class API
    {
        public static class Apartment
        {
            public static string Bedrooms(string baseUri) => $"{baseUri}/bedrooms";
            public static string Countries(string baseUri) => $"{baseUri}/countries";
            public static string Furnishings(string baseUri) => $"{baseUri}/furnishings";
            public static string Periods(string baseUri) => $"{baseUri}/periods";
            public static string Purposes(string baseUri) => $"{baseUri}/purpose";
        }

        public static class Owners
        {
            public static string FullOwners(string baseUri) => $"{baseUri}/owners";
            public static string BasicOwners(string baseUri) => $"{baseUri}/basicowners";
        }
    }
}
