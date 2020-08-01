namespace WebClientAgg.Config
{
    public class UrlsConfig
    {
        public class ApartmentOperations
        {
            public static string GetApartmentById(int id) => $"/api/v1/apartment/{id}";
            public static string GetDropDownLists() => $"/api/v1/apartment/dropdownlists";
        }

        public class OwnersOperations
        {
            public static string GetOwnerById(int id) => $"/api/v1/owner/{id}";
            public static string GetOwners() => $"/api/v1/owner/owners";
        }

        public string Apartment { get; set; }
        public string Owners { get; set; }
        public string GrpcApartment { get; set; }
        public string GrpcOwners { get; set; }
    }
}
