namespace Apartment.API
{
    public class ApartmentSettings
    {
        public string EventBusConnection { get; set; }
        public bool UseCustomizationData { get; set; }
        public bool AzureStorageEnabled { get; set; }
        public long FileSizeLimit { get; set; }
    }
}
