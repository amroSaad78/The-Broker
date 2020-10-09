namespace Identity.API
{
    public class AppSettings
    {
        public string MvcClient { get; set; }
        public string ApartmentApiClient { get; set; }
        public string OwnerApiClient { get; set; }
        public string WebClientAggClient { get; set; }
        public bool UseCustomizationData { get; set; }
        public bool EnableDevspaces { get; set; }
    }
}
