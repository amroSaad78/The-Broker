using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly string _apartmentUrl;
        public OwnersService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _apiClient = httpClient;
            _settings = settings;
            _apartmentUrl = $"{_settings.Value.ApartmentUrl}/api/v1/owners";
        }
        public Task<IEnumerable<SelectListItem>> GetOwners() => throw new NotImplementedException();
    }
}
