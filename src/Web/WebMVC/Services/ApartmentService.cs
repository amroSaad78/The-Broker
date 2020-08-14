using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<ApartmentService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apartmentUrl;
        private readonly string _apartmentAggUrl;
        public ApartmentService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<ApartmentService> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
            _apartmentUrl = $"{_settings.Value.ApartmentUrl}";
            _apartmentAggUrl = $"{_settings.Value.ApartmentAggUrl}/agg/api/v1/apartment";
        }

        public Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take) => throw new NotImplementedException();
        public Task<Apartment> GetApartment(int id) => throw new NotImplementedException();

        public async Task<string> PopulateLists()
        {
            var response = await _httpClient.GetAsync(_apartmentAggUrl);
            _logger.LogDebug("[PopulateLists] -> response code {StatusCode}", response.StatusCode);
            return await response.Content.ReadAsStringAsync();  
        }

        public async Task SaveRent(Rent apartment)
        {
            _logger.LogInformation($"Saving apartment for rent data located in: {apartment.City}");
            await Save(apartment);
        }
        public async Task SaveSale(Sale apartment)
        {
            _logger.LogInformation($"Saving apartment for sale data located in: {apartment.City}");
            await Save(apartment);
        }

        private async Task Save(Apartment apartment)
        {
            var apartmentContent = new StringContent(JsonConvert.SerializeObject(apartment), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            string url= Uris.GetUrl(_apartmentUrl, apartment.GetType().Name);
            if (apartment.Id > 0)
            {
                response = await _httpClient.PutAsync(url, apartmentContent);
            }
            else
            {
                response = await _httpClient.PostAsync(url, apartmentContent);
            }
            _logger.LogInformation($"Response of saving apartment, Status Code: {response.StatusCode}");
            response.EnsureSuccessStatusCode();
        }
        public async Task UploadImage (IFormFile file)
        {
            if (file.Length <= 0) return;
            string url = Uris.GetUrl(_apartmentUrl, "Pic");
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imgData = new MultipartFormDataContent
            {
                { new StreamContent(file.OpenReadStream()), "imgfile", fileName }
            };
            var response = await _httpClient.PostAsync(url, imgData);
            response.EnsureSuccessStatusCode();            
        }
    }
}
