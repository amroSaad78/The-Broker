using BuildingBlocks.EventBus.Events;

namespace Apartment.API.IntegrationEvents.Events
{
    public class SalePriceChangedIntegrationEvent: IntegrationEvent
    {
        public int ApartmentId { get; private set; }

        public decimal NewPrice { get; private set; }

        public decimal OldPrice { get; private set; }
        public SalePriceChangedIntegrationEvent(int apartmentId, decimal newPrice, decimal oldPrice)
        {
            ApartmentId = apartmentId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
