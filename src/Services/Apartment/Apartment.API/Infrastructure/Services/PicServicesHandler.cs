using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Apartment.API.Infrastructure.Services
{
    public class PicServicesHandler: IPicServicesHandler
    {
        private readonly IWebHostEnvironment _env;        

        public PicServicesHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void Subscrib(IPicService picService)
        {
            picService.OnFileUpload += _picService_OnFileUploadAsync;
        }

        private async void _picService_OnFileUploadAsync(object sender, IFormFile file)
        {
            //start to save file locally or on azure making Istore service
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(_env.WebRootPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
