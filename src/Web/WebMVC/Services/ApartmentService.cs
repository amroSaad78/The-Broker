using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Extension;
using WebMVC.Infrastructure.Validators;
using WebMVC.Model;
using WebMVC.Services.Signatures;

namespace WebMVC.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<ApartmentService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apartmentUrl;
        private readonly string _apartmentAggUrl;
        public ApartmentService(HttpClient httpClient, IOptions<AppSettings> options, ILogger<ApartmentService> logger)
        {
            _httpClient = httpClient;
            _options = options;
            _logger = logger;
            _apartmentUrl = $"{_options.Value.ApartmentUrl}";
            _apartmentAggUrl = $"{_options.Value.ApartmentAggUrl}/agg/api/v1/apartment";
        }

        public Task<Apartment> GetAllApartment(ApplicationUser user, int page, int take) => throw new NotImplementedException();
        public Task<Apartment> GetApartment(int id) => throw new NotImplementedException();

        public async Task<string> PopulateLists()
        {
            var response = await _httpClient.GetAsync(_apartmentAggUrl);
            _logger.LogDebug("[PopulateLists] -> response code {StatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task Save(Payload<IPayload> payload)
        {
            _logger.LogInformation($"Saving apartment for rent data located in: {payload.Apartment.City}");
            var apartmentContent = new StringContent(JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            string url= Uris.GetUrl(_apartmentUrl, payload.InObject.GetType().Name);
            if (payload.Apartment.Id > 0)
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

        public async Task UploadImage (IFormFile file, Guid requestId)
        {
            if (file == null) return;
            var rule = new IsFileNotNull().And(new IsFileSizeSuitable(_options)).And(new IsFileExtntionSuitable()).And(new IsFileSignatureSuitable());
            if (!rule.IsSatisfiedBy(file)) throw new ArgumentException("Data saved, but file size should less than 2Mb and type should be [JPG, JPEG, PNG].");
            string url = Uris.GetUrl(_apartmentUrl, "Pic");
            var imgData = new MultipartFormDataContent
            {
                { new StreamContent(file.OpenReadStream()), "imgfile", file.FileName }
            };
            _httpClient.DefaultRequestHeaders.Add("x-requestid", requestId.ToString());
            var response = await _httpClient.PostAsync(url, imgData);
            _logger.LogDebug("[UploadImage] -> response code {StatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();
        }
    }
}
