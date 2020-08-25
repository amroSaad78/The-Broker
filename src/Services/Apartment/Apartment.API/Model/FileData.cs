using Microsoft.AspNetCore.Http;
using System;

namespace Apartment.API.Model
{
    public class FileData
    {
        public FileData(IFormFile file, Guid requestId)
        {
            File = file;
            RequestId = requestId;
        }
        public IFormFile File { get; set; }
        public Guid RequestId { get; set; }
    }

    public class SavedFileInfo
    {
        public SavedFileInfo(Guid requestId, string pictureUri, string pictureFileName)
        {
            RequestId = requestId;
            PictureUri = pictureUri;
            PictureFileName = pictureFileName;
        }
        public Guid RequestId { get; set; }
        public string PictureUri { get; set; }
        public string PictureFileName { get; set; }
    }
}
