using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Extension;
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
            var response = await _httpClient.GetStringAsync(url);
            return response.GetSelectListAsync("bedroomsCount");
        }

        public async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            var url = API.Apartment.Countries(_apartmentUrl);
            var response = await _httpClient.GetStringAsync(url);
            return response.GetSelectListAsync("country");
        }
        public async Task<IEnumerable<SelectListItem>> GetFurnishings()
        {
            var url = API.Apartment.Furnishings(_apartmentUrl);
            var response = await _httpClient.GetStringAsync(url);
            return response.GetSelectListAsync("furnitureType");
        }
        public async Task<IEnumerable<SelectListItem>> GetPeriods()
        {
            var url = API.Apartment.Periods(_apartmentUrl);
            var response = await _httpClient.GetStringAsync(url);
            return response.GetSelectListAsync("period");
        }
        public async Task SaveApartment(Apartment apartment)
        {
            _logger.LogInformation($"Saving apartment data located in: {apartment.City}");
            var apartmentContent = new StringContent(JsonConvert.SerializeObject(apartment), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            if (apartment.Id > 0)
            {
                response = await _httpClient.PutAsync(_apartmentUrl, apartmentContent);
            }
            else
            {
                response = await _httpClient.PostAsync(_apartmentUrl, apartmentContent);
            }
            response.EnsureSuccessStatusCode();
        }
    }
}
