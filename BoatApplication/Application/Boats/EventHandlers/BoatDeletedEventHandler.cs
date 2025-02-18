using BoatApplication.Domain.Boats.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.EventHandlers
{
    public class BoatDeletedEventHandler : INotificationHandler<BoatDeletedEvent>
    {
        private readonly ILogger<BoatDeletedEventHandler> _logger;
        public BoatDeletedEventHandler(ILogger<BoatDeletedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(BoatDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Boat {notification.Boat.Id} was deleted.");
            return Task.CompletedTask;
        }
    }
}
