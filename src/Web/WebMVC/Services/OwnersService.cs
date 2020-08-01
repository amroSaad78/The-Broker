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

        public async Task<IEnumerable<SelectListItem>> GetBasicOwners()
        {
            var url = API.Owners.BasicOwners(_ownersUrl);
            return await GetSelectListAsync(url, "FullName");
        }
        Task<IEnumerable<Owner>> IOwnersService.GetOwners() => throw new NotImplementedException();

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
