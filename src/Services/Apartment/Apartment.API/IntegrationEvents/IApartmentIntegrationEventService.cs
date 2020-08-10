using BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Apartment.API.IntegrationEvents
{
    public interface IApartmentIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
