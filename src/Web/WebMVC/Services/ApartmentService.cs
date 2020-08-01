using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Model;

namespace WebMVC.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<ApartmentService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apartmentUrl;
        public ApartmentService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<ApartmentService> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
            _apartmentUrl = $"{_settings.Value.ApartmentUrl}/ap/api/v1/apartment";
        }

        public Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take) => throw new NotImplementedException();
        public Task<Apartment> GetApartment(int id) => throw new NotImplementedException();
        public async Task<IEnumerable<SelectListItem>> GetBedrooms()
        {
            var url = API.Apartment.Bedrooms(_apartmentUrl);
            return await GetSelectListAsync(url, "BedroomsCount");
        }

        public async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            var url = API.Apartment.Countries(_apartmentUrl);
            return await GetSelectListAsync(url, "Country");
        }
        public async Task<IEnumerable<SelectListItem>> GetFurnishings()
        {
            var url = API.Apartment.Furnishings(_apartmentUrl);
            return await GetSelectListAsync(url, "FurnitureType");
        }
        public async Task<IEnumerable<SelectListItem>> GetPeriods()
        {
            var url = API.Apartment.Periods(_apartmentUrl);
            return await GetSelectListAsync(url, "Period");
        }
        public async Task<IEnumerable<SelectListItem>> GetPurpose()
        {
            var url = API.Apartment.Purposes(_apartmentUrl);
            return await GetSelectListAsync(url, "PurposeType");
        }

        private async Task<IEnumerable<SelectListItem>> GetSelectListAsync(string url, string text)
        {
            var response = await _httpClient.GetStringAsync(url);
            if (string.IsNullOrEmpty(response)) return null;
            var list = new List<SelectListItem>();
            var json = JArray.Parse(response);
            foreach (var item in json.Children<JObject>())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Value<string>("Id"),
                    Text = item.Value<string>(text)
                });
            }
            return list;
        }
    }
}
