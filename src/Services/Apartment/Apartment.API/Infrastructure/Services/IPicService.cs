using Microsoft.AspNetCore.Http;
using System;

namespace Apartment.API.Infrastructure.Services
{
    public interface IPicService
    {
        event EventHandler<IFormFile> OnFileUpload;
        void UploadFile(IFormFile file);
    }
}
