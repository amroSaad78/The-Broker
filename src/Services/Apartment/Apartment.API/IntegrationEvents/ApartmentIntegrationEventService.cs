using Apartment.API.Infrastructure;
using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBus.Events;
using BuildingBlocks.IntegrationEventLogEF.Services;
using BuildingBlocks.IntegrationEventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Apartment.API.IntegrationEvents
{
    public class ApartmentIntegrationEventService : IApartmentIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly ApartmentContext _apartmentContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<ApartmentIntegrationEventService> _logger;
        public ApartmentIntegrationEventService(
            ILogger<ApartmentIntegrationEventService> logger,
            IEventBus eventBus,
            ApartmentContext apartmentContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apartmentContext = apartmentContext ?? throw new ArgumentNullException(nameof(apartmentContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(apartmentContext.Database.GetDbConnection());
        }
        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }
        public async Task SaveEventAndApartmentContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_apartmentContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original apartment database operation and the IntegrationEventLog thanks to a local transaction
                await _apartmentContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _apartmentContext.Database.CurrentTransaction);
            });
        }
    }
}
