using BuildingBlocks.EventBus.Events;

namespace Apartment.API.IntegrationEvents.Events
{
    public class RentPriceChangedIntegrationEvent: IntegrationEvent
    {
        public int ApartmentId { get; private set; }

        public decimal NewPrice { get; private set; }

        public decimal OldPrice { get; private set; }
        public RentPriceChangedIntegrationEvent(int apartmentId, decimal newPrice, decimal oldPrice)
        {
            ApartmentId = apartmentId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
