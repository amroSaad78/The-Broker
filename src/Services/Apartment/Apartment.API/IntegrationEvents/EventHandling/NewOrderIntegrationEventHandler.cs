using Apartment.API.IntegrationEvents.Events;
using BuildingBlocks.EventBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace Apartment.API.IntegrationEvents.EventHandling
{
    public class NewOrderIntegrationEventHandler : IIntegrationEventHandler<NewOrderIntegrationEvent>
    {
        public Task Handle(NewOrderIntegrationEvent @event) => throw new NotImplementedException();
    }
}
