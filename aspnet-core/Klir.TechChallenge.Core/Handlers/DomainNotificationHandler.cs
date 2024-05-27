using Klir.TechChallenge.Core.Events;
using Klir.TechChallenge.Core.Notifications;
using MediatR;

namespace Klir.TechChallenge.Core.Handlers
{
    public sealed class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private readonly INotificator _notificator;

        public DomainNotificationHandler(INotificator notificator)
        {
            _notificator = notificator;
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notificator.AddMessages(notification.Messages.ToArray());
            return Task.CompletedTask;
        }
    }
}
