using BoatApplication.Domain.Boats.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.EventHandlers
{
    public class BoatCreatedEventHandler : INotificationHandler<BoatCreatedEvent>
    {
        private readonly ILogger<BoatCreatedEventHandler> _logger;
        public BoatCreatedEventHandler(ILogger<BoatCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(BoatCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Boat {notification.Boat.Id} was created.");
            return Task.CompletedTask;
        }
    }
}
