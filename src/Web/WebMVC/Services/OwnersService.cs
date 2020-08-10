using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Services
{
    public class OwnersService : IOwnersService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly string _ownersUrl;
        public OwnersService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _ownersUrl = $"{_settings.Value.OwnersUrl}/ow/api/v1/owner";
        }
        Task<IEnumerable<Owner>> IOwnersService.GetOwners() => throw new NotImplementedException();
    }
}
