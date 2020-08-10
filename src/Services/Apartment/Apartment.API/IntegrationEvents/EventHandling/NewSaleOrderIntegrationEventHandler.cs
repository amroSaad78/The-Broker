using Apartment.API.IntegrationEvents.Events;
using BuildingBlocks.EventBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace Apartment.API.IntegrationEvents.EventHandling
{
    public class NewSaleOrderIntegrationEventHandler : IIntegrationEventHandler<NewSaleOrderIntegrationEvent>
    {
        public Task Handle(NewSaleOrderIntegrationEvent @event) => throw new NotImplementedException();
    }
}
