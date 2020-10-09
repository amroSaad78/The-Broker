using Apartment.API.Model;
using System;

namespace Apartment.API.Infrastructure.Services
{
    public interface IPicService
    {
        event EventHandler<FileData> OnFileUploaded;
        void UploadFile(FileData fileData);
    }
}
