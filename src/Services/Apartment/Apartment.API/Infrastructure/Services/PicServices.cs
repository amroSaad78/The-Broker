using Microsoft.AspNetCore.Http;
using System;

namespace Apartment.API.Infrastructure.Services
{
    public class PicServices : IPicService
    {
        public event EventHandler<IFormFile> OnFileUpload;

        public void UploadFile(IFormFile file) => OnFileUpload?.Invoke(this, file);
    }
}
