using Apartment.API.Model;
using System;

namespace Apartment.API.Infrastructure.Services
{
    public class PicService : IPicService
    {        
        public event EventHandler<FileData> OnFileUploaded;
        public void UploadFile(FileData fileData) => OnFileUploaded?.Invoke(this, fileData);
    }
}
