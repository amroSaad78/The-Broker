using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly HttpClient _apiClient;
        private readonly string _apartmentUrl;
        public ApartmentService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _apiClient = httpClient;
            _settings = settings;
            _apartmentUrl = $"{_settings.Value.ApartmentUrl}/api/v1/apartment";
        }
        public Apartment GetNewApartment()
        {
            return new Apartment() { };
        }
        public Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take) => throw new NotImplementedException();
        public Task<Apartment> GetApartment(int id) => throw new NotImplementedException();
        public async Task<IEnumerable<SelectListItem>> GetBedrooms()
        {
            var uri = API.Apartment.Bedrooms(_apartmentUrl);
            return await GetSelectList(uri, "BedroomsNo");
        }
        public async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            var uri = API.Apartment.Countries(_apartmentUrl);
            return await GetSelectList(uri, "CountryName");
        }
        public async Task<IEnumerable<SelectListItem>> GetFurnishings()
        {
            var uri = API.Apartment.Furnishings(_apartmentUrl);            
            return await GetSelectList(uri,"Status");
        }
        public async Task<IEnumerable<SelectListItem>> GetOwners()
        {
            var uri = API.Apartment.Owners(_apartmentUrl);
            return await GetSelectList(uri, "Name");
        }
        public async Task<IEnumerable<SelectListItem>> GetPeriods()
        {
            var uri = API.Apartment.Periods(_apartmentUrl);
            return await GetSelectList(uri, "PeriodName");
        }
        public async Task<IEnumerable<SelectListItem>> GetPurposes()
        {
            var uri = API.Apartment.Periods(_apartmentUrl);
            return await GetSelectList(uri, "PurposeName");
        }
        private async Task<IEnumerable<SelectListItem>> GetSelectList(string uri, string text)
        {
            var responseString = await _apiClient.GetStringAsync(uri);
            if (string.IsNullOrEmpty(responseString)) throw new NullReferenceException($"{uri}: Null or empty result");
            List<SelectListItem> list = new List<SelectListItem>();
            var response = JArray.Parse(responseString);
            foreach (var item in response.Children<JObject>())
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
