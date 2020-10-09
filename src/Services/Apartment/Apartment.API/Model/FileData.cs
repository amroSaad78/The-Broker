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
        public IFormFile File { get; private set; }
        public Guid RequestId { get; private set; }
    }
}
