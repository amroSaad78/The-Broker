using Apartment.API.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Services
{
    public class LocalStorage : IStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<LocalStorage> _logger;

        public LocalStorage(IWebHostEnvironment env, ILogger<LocalStorage> logger)
        {
            _env = env;            
            _logger = logger;
        }
        public async Task SaveAsync(FileData fileData)
        {
            string fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileData.File.FileName)}";
            var filePath = Path.Combine(_env.WebRootPath, fileName);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    Task copy =Task.Run(async() =>await fileData.File.CopyToAsync(stream));
                    copy.Wait();
                    if (copy.IsCompleted) {
                        //fire filesaved event
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"[Uploading failed] - ex: {ex.Message}");
                //invok filenotsaved event on rabbitmq and using signalr to send message to the user;
            }
        }
    }
}
