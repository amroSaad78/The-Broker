using Apartment.API.Infrastructure.Commands;
using Apartment.API.IntegrationEvents.Events;
using Apartment.API.Model;
using Azure.Storage.Blobs;
using BuildingBlocks.EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Services
{
    public class AzureStorage : IStorage
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AzureStorage> _logger;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;
        private readonly IConfiguration _configuration;
        private readonly string azureStorageConnectionString;
        private readonly string azureStorageContainerName;
        private readonly string azureStorageAccountEndpoint;
        public AzureStorage(IMediator mediator,
                            ILogger<AzureStorage> logger,
                            IIdentityService identityService,
                            IEventBus eventBus,
                            IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
            _identityService = identityService;
            _eventBus = eventBus;
            _configuration = configuration;
            azureStorageConnectionString = _configuration["AzureStorageConnectionString"];
            azureStorageContainerName = _configuration["AzureStorageContainerName"];
            azureStorageAccountEndpoint = _configuration["AzureStorageAccountEndpoint"];
        }

        public async Task InitAsync()
        {
            BlobContainerClient container = new BlobContainerClient(azureStorageConnectionString, azureStorageContainerName);
            await container.CreateIfNotExistsAsync();
        }
        public async Task SaveAsync(FileData fileData)
        {
            string fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileData.File.FileName)}";
            string filePath = $"{azureStorageAccountEndpoint}/{azureStorageContainerName}";
            BlobContainerClient container = new BlobContainerClient(azureStorageConnectionString, azureStorageContainerName);
            BlobClient blob = container.GetBlobClient(fileName);
            try
            {
                await blob.UploadAsync(fileData.File.OpenReadStream());
                if (!await _mediator.Send(new UpdatePicCommand(filePath, fileName, fileData.RequestId)))
                    await blob.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);
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
