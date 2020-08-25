using Apartment.API.Model;
using System;

namespace Apartment.API.Infrastructure.Services
{
    public class PicServices : IPicService
    {        
        public event EventHandler<FileData> OnFileUpload;
        public void UploadFile(FileData fileData) => OnFileUpload?.Invoke(this, fileData);
    }

    public class PicDBServices : IPicDBService
    {
        public event EventHandler<SavedFileInfo> OnFileSaved;
        public void UpdateApartmentPic(SavedFileInfo fileInfo) => OnFileSaved?.Invoke(this, fileInfo);
    }
}
