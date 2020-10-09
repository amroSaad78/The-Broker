using BuildingBlocks.EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace SignalrHub.IntegrationEvents
{
    public class UploadingFailedIntegrationEventHandler : IIntegrationEventHandler<UploadingFailedIntegrationEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<UploadingFailedIntegrationEventHandler> _logger;

        public UploadingFailedIntegrationEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<UploadingFailedIntegrationEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task Handle(UploadingFailedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                await _hubContext.Clients
                    .Group(@event.UserId)
                    .SendAsync("UploadingFailed", new { @event.Message});
            }
        }
    }
}
