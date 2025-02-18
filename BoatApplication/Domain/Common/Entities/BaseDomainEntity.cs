using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Entities
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        #region Domain Events
        private readonly List<BaseDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(BaseDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void RemoveDomainEvent(BaseDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
        #endregion
    }
}
