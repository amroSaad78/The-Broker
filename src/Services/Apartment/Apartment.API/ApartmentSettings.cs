using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apartment.API
{
    public class ApartmentSettings
    {
        public string PicBaseUrl { get; set; }

        public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }

        public bool AzureStorageEnabled { get; set; }
    }
}
