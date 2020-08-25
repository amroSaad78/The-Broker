using Apartment.API.Model;
using Microsoft.Extensions.Logging;

namespace Apartment.API.Infrastructure.Services
{
    public class PicDBServicesHandler: IPicDBServicesHandler
    {
        private readonly ILogger<PicDBServicesHandler> _logger;
        private readonly ApartmentContext _context;

        public PicDBServicesHandler(ILogger<PicDBServicesHandler> logger, ApartmentContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void Subscrib(IPicDBService picDBService) => picDBService.OnFileSaved += PicDBService_OnFileSaved;
        private void PicDBService_OnFileSaved(object sender, SavedFileInfo fileInfo)
        {
            _logger.LogInformation($"[Picture file Name] - {fileInfo.PictureFileName}");
        }
    }
}
