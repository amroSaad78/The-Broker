using BuildingBlocks.EventBus.Events;

namespace SignalrHub.IntegrationEvents
{
    public class UploadingFailedIntegrationEvent: IntegrationEvent
    {
        public string UserId { get; private set; }
        public string Message { get; private set; }

        public UploadingFailedIntegrationEvent(string userId, string message)
        {
            UserId = userId;
            Message = message;
        }
    }
}
