using Apartment.API.IntegrationEvents.Events;
using BuildingBlocks.EventBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace Apartment.API.IntegrationEvents.EventHandling
{
    public class NewRentOrderIntegrationEventHandler : IIntegrationEventHandler<NewRentOrderIntegrationEvent>
    {
        public Task Handle(NewRentOrderIntegrationEvent @event) => throw new NotImplementedException();
    }
}
