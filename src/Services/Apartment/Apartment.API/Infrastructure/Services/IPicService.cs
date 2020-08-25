using Apartment.API.Model;
using System;

namespace Apartment.API.Infrastructure.Services
{
    public interface IPicService
    {
        event EventHandler<FileData> OnFileUpload;
        void UploadFile(FileData fileData);
    }

    public interface IPicDBService
    {
        event EventHandler<SavedFileInfo> OnFileSaved;
        void UpdateApartmentPic(SavedFileInfo fileInfo);
    }
}
