using Apartment.API.Infrastructure.Commands;
using Apartment.API.IntegrationEvents.Events;
using Apartment.API.Model;
using BuildingBlocks.EventBus.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Services
{
    public class LocalStorage : IStorage
    {
        private readonly IWebHostEnvironment _env;        
        private readonly IMediator _mediator;
        private readonly ILogger<LocalStorage> _logger;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;

        public LocalStorage(IWebHostEnvironment env,
                            IMediator mediator,
                            ILogger<LocalStorage> logger,
                            IIdentityService identityService,
                            IEventBus eventBus)
        {
            _env = env;                        
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
            _identityService = identityService;
            _eventBus = eventBus;
        }
        public async Task SaveAsync(FileData fileData)
        {
            string fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileData.File.FileName)}";
            var filePath = Path.Combine(_env.WebRootPath, fileName);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await fileData.File.CopyToAsync(stream);
                }
                if(!await _mediator.Send(new UpdatePicCommand(filePath, fileName, fileData.RequestId)))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError($"The uploading process has failed to request No. {fileData.RequestId} - Ex : {ex.Message}");
                
                _eventBus.Publish(new UploadingFailedIntegrationEvent(_identityService.GetUserIdentity(),
                                                                        $"The uploading process has failed to request No. {fileData.RequestId}"));
            }
        }
    }
}
