using BoatApplication.Domain.Boats.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.EventHandlers
{
    public class BoatModifiedEventHandler : INotificationHandler<BoatModifiedEvent>
    {
        private readonly ILogger<BoatModifiedEventHandler> _logger;
        public BoatModifiedEventHandler(ILogger<BoatModifiedEventHandler> logger)
        {
            _logger = logger;
        }

        private string ShowComparison(string title, string? str1, string? str2)
        {
            return str1 != str2 ? $"{title} : {str1} -> {str2}" : "";
        }
        public Task Handle(BoatModifiedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Boat {notification.CurrentState.Id} was modified. {
                ShowComparison("Name", notification.OldState.Name, notification.CurrentState.Name)
                } {
                ShowComparison("Description", notification.OldState.Description, notification.CurrentState.Description)
                }");
            return Task.CompletedTask;
        }
    }
}
