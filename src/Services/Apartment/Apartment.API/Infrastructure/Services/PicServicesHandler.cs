using Apartment.API.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Apartment.API.Infrastructure.Services
{
    public class PicServicesHandler: IPicServicesHandler
    {
        private readonly IEnumerable<IStorage> _storages;
        private readonly IOptions<ApartmentSettings> _options;

        public PicServicesHandler(IEnumerable<IStorage> storages, IOptions<ApartmentSettings> options)
        {
            _storages = storages;
            _options = options;
        }

        public void Subscrib(IPicService picService) => picService.OnFileUpload += PicService_OnFileUploadAsync;

        private async void PicService_OnFileUploadAsync(object sender, FileData fileData)
        {
            //just using local and azure blob storage;
            IStorage storage = _options.Value.AzureStorageEnabled ? 
                                        _storages.FirstOrDefault(s => s.GetType().Name == nameof(AzureStorage)):
                                        _storages.FirstOrDefault(s => s.GetType().Name == nameof(LocalStorage));
            await storage.SaveAsync(fileData);
        }
    }
}
